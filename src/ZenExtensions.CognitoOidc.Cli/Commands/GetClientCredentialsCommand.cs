using System.Text;
using System.ComponentModel;
using Refit;
using ZenExtensions.CognitoOidc.Cli.Apis;
using Humanizer;

namespace ZenExtensions.CognitoOidc.Cli.Commands;

public class GetClientCredentialsCommand : ZenAsyncCommand<GetClientCredentialsCommandSettings>
{
    public override async Task OnExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        var tokenBytes = Encoding.UTF8.GetBytes($"{Settings.CognitoId}:{Settings.CognitoSecret}");
        var token = $"Basic {Convert.ToBase64String(tokenBytes)}";
        var api = RestService.For<ICognitoApi>(Settings.Domain);
        try
        {
            var response = await Terminal.StatusAsync("Getting token...", async _ => 
            {
                return await api.GetTokenAsync(
                    new AccessTokenResquest(
                        GrantType: "client_credentials",
                        ClientId: Settings.CognitoId
                    ),
                    authorizationToken: token
                );
            });

            var expiry = TimeSpan.FromSeconds(response.ExpiresIn);

            Terminal.WriteLine(response.AccessToken);
            Terminal.WriteLine("");
            Terminal.WriteInfo($"This token will expire in {expiry.Humanize()}");
        }
        catch(ApiException ex)
        {
            var errorObject = await ex.GetContentAsAsync<CognitoError>();
            Terminal.WriteError($"{errorObject.Error}, please check if you've correct cognito id and secret");
        }
    }
}

public class GetClientCredentialsCommandSettings : ZenCommandSettings
{
    [Description("Domain of cognito user pool")]
    [CommandOption("-d|--domain")]
    public string Domain { get; set; } = default!;
    [Description("Cognito id for the request")]
    [CommandOption("-i|--cognito-id")]
    public string CognitoId { get; set; } = default!;
    [Description("Cognito secret for the request")]
    [CommandOption("-s|--cognito-secret")]
    public string CognitoSecret { get; set; } = default!;

    public override ValidationResult Validate()
    {
        if(string.IsNullOrWhiteSpace(Domain))
        {
            return ValidationResult.Error("--domain must be provided");
        }
        if(!Uri.TryCreate(Domain, UriKind.Absolute, out var uriResult))
        {
            return ValidationResult.Error("--domain isn't a proper url");
        }
        if (uriResult.Scheme != Uri.UriSchemeHttps)
        {
            return ValidationResult.Error("--domain must be a proper https url");
        }
        if(string.IsNullOrWhiteSpace(CognitoId))
        {
            return ValidationResult.Error("--cognito-id must be provided");
        }
        if(string.IsNullOrWhiteSpace(CognitoSecret))
        {
            return ValidationResult.Error("--cognito-secret must be provided");
        }
        return ValidationResult.Success();
    }
}
