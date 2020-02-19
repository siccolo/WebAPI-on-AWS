using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Extensions;

namespace Service
{
    public sealed class RedeemService: IService<Service.RedeemCodeRequest, Models.RedeemResult>// IService<Models.RedeemCode, Models.RedeemResult>
    {
        private readonly Datastore.RCDBContext _dbcontext;

        private readonly ILogger<RedeemService> _logger;
        public RedeemService(Datastore.RCDBContext db, ILogger<RedeemService> logger)
        {
            _dbcontext = db ?? throw new System.ArgumentNullException("dbcontext");
            _logger = logger;
        }

        public async Task<Models.RedeemResult> Process(Service.RedeemCodeRequest request)
        {
            var code = request.RedeemCode?? throw new System.ArgumentNullException("code");

            //Looks up code in MySql RDS database and then determines result
            var redeemResult = await this.LookupRedeemCodeResult(code).ConfigureAwait(false);

            //update result as redeemed
            if (redeemResult.Success && redeemResult.Result.Value == Models.RedeemResultEnum.Success.Value)
            {
                var updated = await UpdateRedeemCodeStatus(code).ConfigureAwait(false);
                if (!updated.Success)
                {
                    //oh-ho....
                }
            }            

            //When API method is called, write the attempt into a separate table that will have the data we need for final report
            var logged = await LogLookup(code, redeemResult, request.CallerInfo).ConfigureAwait(false);
            if (!logged.Success) { return new Models.RedeemResult(logged.Exception, logged.AdditionalInfo); }
            

            return redeemResult;
        }

        private async Task<Models.BoolResult> LogLookup(Models.RedeemCode code, Models.RedeemResult resultCodeRedeem, Models.CallerInfo caller)
        {
            var newLogEntry = new DataModels.RedeemLogDbEntry(code, resultCodeRedeem, caller);

            try
            {
                await _dbcontext.RedeemLogDbEntry.AddAsync(newLogEntry).ConfigureAwait(false);
                var issaved = await _dbcontext.SaveChangesAsync().ConfigureAwait(false);
                return new Models.BoolResult(issaved==1);
            }
            catch (DbUpdateException ex)
            {
                return new Models.BoolResult(ex, Extensions.Constants.FailedToSave);
            }
            catch (Exception ex)
            {
                /*
                Exceptions
                DbUpdateException An error occurred sending updates to the database.

                DbUpdateConcurrencyException A database command did not affect the expected number of rows. This usually indicates an optimistic concurrency violation; that is, a row has been changed in the database since it was queried.

                DbEntityValidationException The save was aborted because validation of entity property values failed.

                NotSupportedException An attempt was made to use unsupported behavior such as executing multiple asynchronous commands concurrently on the same context instance.

                ObjectDisposedException The context or connection have been disposed.

                InvalidOperationException Some error occurred attempting to process entities in the context either before or after sending commands to the database.
                */
                return new Models.BoolResult(ex, Extensions.Constants.UnknownFailure);
            }
        }
        
        private async Task<Models.RedeemResult> LookupRedeemCodeResult(Models.RedeemCode code)
        {
            try
            {
                //
                //var query = _dbcontext.RedeemCodeDbEntry.Where(r => r.RedeemCode == code.Value);
                //var sql = query.ToSql();

                //  -- use the view
                var record = await _dbcontext.vwRedeemCode.AsNoTracking().FirstOrDefaultAsync(r => r.RedeemCode == code.Value).ConfigureAwait(false);

                if (record != null )
                {
                    return new Models.RedeemResult(record.RedeemResult);
                }
                else
                {
                    return new Models.RedeemResult(Models.RedeemResultEnum.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new Models.RedeemResult(ex, Extensions.Constants.FailedToRetrieve);
            }
        }

        private async Task<Models.BoolResult> UpdateRedeemCodeStatus(Models.RedeemCode code)
        {
            var newLogEntry = new DataModels.RedeemCodeRedeemResultDbEntry(code);

            try
            {
                await _dbcontext.RedeemCodeRedeemResultDbEntry.AddAsync(newLogEntry).ConfigureAwait(false);
                var issaved = await _dbcontext.SaveChangesAsync().ConfigureAwait(false);
                return new Models.BoolResult(issaved == 1);
            }
            catch (DbUpdateException ex)
            {
                return new Models.BoolResult(ex, Extensions.Constants.FailedToSave);
            }
            catch (Exception ex)
            {
                /*
                Exceptions
                DbUpdateException An error occurred sending updates to the database.

                DbUpdateConcurrencyException A database command did not affect the expected number of rows. This usually indicates an optimistic concurrency violation; that is, a row has been changed in the database since it was queried.

                DbEntityValidationException The save was aborted because validation of entity property values failed.

                NotSupportedException An attempt was made to use unsupported behavior such as executing multiple asynchronous commands concurrently on the same context instance.

                ObjectDisposedException The context or connection have been disposed.

                InvalidOperationException Some error occurred attempting to process entities in the context either before or after sending commands to the database.
                */
                return new Models.BoolResult(ex, Extensions.Constants.UnknownFailure);
            }
        }
    }
}
