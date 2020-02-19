Notes on using AWS SDK:

Download/Install AWS SDK for Visual Studio from https://aws.amazon.com/visualstudio/

From AWS console (http://console.aws.amazon.com), 
	-> Identity and Access Management (IAM) (https://console.aws.amazon.com/iam/) 
		-> Users (https://console.aws.amazon.com/iam/home#/users)
			-> Add User


in VS, File -> New -> Project -> AWS Serverless Application -> ....


Publishing:
dotnet lambda deploy-serverless 