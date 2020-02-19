using System;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace xUnitestBackendHost
{
    public class UnitTest_RedeemCode
    {
        private readonly ITestOutputHelper _Output;

        Service.RedeemService _Service;
        Datastore.RCDBContext _DBContext;
        Models.CallerInfo _Caller;

        public UnitTest_RedeemCode(ITestOutputHelper output)
        {
            _Output = output;

            _Caller = new Models.CallerInfo() { IP = "8.8.8.1", UserAgent = "Mozilla#1" };

            var dbconfig = Helpers.AppSettingsExtensions.GetApplicationConfiguration(AppContext.BaseDirectory);
            var connectioninfo = dbconfig.ConnectionInfo;
            var optionsBuilder = new DbContextOptionsBuilder<Datastore.RCDBContext>();
            optionsBuilder.UseMySql(connectioninfo);
            _DBContext = new Datastore.RCDBContext(optionsBuilder.Options);
            _Service = new Service.RedeemService(_DBContext, null);
        }

        [Theory]
        [InlineData("123456789")]
        public async Task Test_ProcessOne_Success(string code)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var codedata = new Models.RedeemCode(code);
            var request = new Service.RedeemCodeRequest(codedata, _Caller);
            var result = await _Service.Process(request).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine  ( $"Test_ProcessOne timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.Result.Value== Models.RedeemResultEnum.Success.Value);
        }
        [Theory]
        [InlineData("987654321")]
        public async Task Test_ProcessOne_AlreadyRedeemed(string code)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var codedata = new Models.RedeemCode(code);
            var request = new Service.RedeemCodeRequest(codedata, _Caller);
            var result = await _Service.Process(request).ConfigureAwait(false);
            
            sw.Stop();
            _Output.WriteLine($"Test_ProcessOne timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.Result.Value == Models.RedeemResultEnum.AlreadyRedeemed.Value);
        }

        [Theory]
        [InlineData("123456789")]
        
        [InlineData("000000000")]
        [InlineData("        1")]
        [InlineData("100000000")]
        [InlineData("000111222")]
        [InlineData("999888777")]

        [InlineData("123456780")]
        [InlineData("987654320")]
        [InlineData("123456781")]
        [InlineData("987654322")]

        [InlineData("123456782")]
        [InlineData("987654323")]
        [InlineData("123456784")]
        [InlineData("987654325")]

        [InlineData("223456782")]
        [InlineData("387654323")]
        [InlineData("423456784")]
        [InlineData("587654325")]

        [InlineData("987654321")]
        public async Task Test_ProcessMany(string code)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var codedata = new Models.RedeemCode(code);
            var request = new Service.RedeemCodeRequest(codedata, _Caller);
            var result = await _Service.Process(request).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"Test_ProcessOne timing (code):" + sw.ElapsedMilliseconds.ToString() + " result:" + result.Result.Value);

            Assert.True(result.Success);//&& result.Result == Models.RedeemResultEnum.Sucess);
        }
    }
}
