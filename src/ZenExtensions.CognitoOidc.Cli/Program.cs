using ZenExtensions.CognitoOidc.Cli;

await new CliHost("cognito-oidc")
    .Configure(c => {
        c.Spinner = Spinner.Known.Arc;
        c.TableBorder = TableBorder.Markdown;
    })
    .RunAsync<Startup>(args);