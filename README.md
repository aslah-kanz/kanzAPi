# Configuration

| Key                              | Description                       | Default Value        |
| -------------------------------- | --------------------------------- | -------------------- |
| KanzApi.Db.ConnectionString      | DB connection string              |                      |
| KanzApi.BaseUrl                  | App base URL                      | https://api.kanzway.com/v1, https://api-dev.kanzway.com/v1 (dev) |
| KanzApi.IpAddress                | App IP address                    |                      |
| KanzApi.CorsPolicyOrigins        | List of allowed CORS              | &#91;https://kanzway.com, https://www.kanzway.com&#93;, &#91;https://dev.kanzway.com&#93; (dev) |
| KanzApi.Jwt.Audience             | Host address                      | https://api.kanzway.com, https://api-dev.kanzway.com (dev) |
| KanzApi.Jwt.Issuer               | Host address                      | https://api.kanzway.com, https://api-dev.kanzway.com (dev) |
| KanzApi.Jwt.SigningKey           | JWT signing key (min. length 32)  |                      |
| KanzApi.Jwt.Validity             | Access token validity in seconds  | 300, 3,600 (dev)     |
| KanzApi.Jwt.RefreshTokenValidity | Refresh token validity in seconds | 86,400               |
| KanzApi.Mail.Sender              | Mail sender                       | no-reply@kanzway.com |
| KanzApi.Activation.Validity      | Activation validity in seconds    | 3,600                |
| KanzApi.Otp.MaxAttempt           | OTP maximum attempt               | 3                    |
| KanzApi.Otp.Validity             | OTP validity in seconds           | 180                  |
| KanzApi.StorageDir               | File storage directory            | ./data               |
| KanzApi.Logging.MaskedKeys       | Masked keys                       | ["authorization", "password", "pin", "token"] |
| KanzApi.Logging.SensitiveDataMasked | Mask log sensitive data           | true                 |
| AzureStorage.ConnectionString    | Azure Storage connection string   |                      |
| AzureStorage.BaseUrl             | Azure Storage base URL            |                      |
| Graph.Login.BaseUrl              | Graph Login base URL              | https://login.microsoftonline.com |
| Graph.Mail.BaseUrl               | Graph Mail base URL               | https://graph.microsoft.com |
| Graph.Mail.ClientId              | Graph Mail client ID              |                      |
| Graph.Mail.ClientSecret          | Graph Mail client secret          |                      |
| Graph.Mail.TenantId              | Graph Mail tenant ID              |                      |
| Meilisearch.MasterKey            | Meilisearch master key            |                      |
| Msegat.BaseUrl                   | Msegat base URL                   | https://www.msegat.com |
| Msegat.Username                  | Msegat username                   |                      |
| Msegat.ApiKey                    | Msegat API key                    |                      |
| Msegat.Sender                    | Msegat sender                     |                      |
| Oto.BaseUrl                      | Oto base URL                      | https://api.tryoto.com, https://staging-api.tryoto.com (dev) |
| Oto.ShipmentEnabled              | Oto enable shipment               | true                 |
| Oto.RefreshToken                 | Oto refresh token                 |                      |
| Oto.ApiAuthKey                   | Oto API authorization key         |                      |
| Oto.ApiAuthValue                 | Oto API authorization value       |                      |
| Oto.Company.Whitelist            | Oto API company whitelist         |                      |
| SendGrid.BaseUrl                 | SendGrid base URL                 | https://api.sendgrid.com |
| SendGrid.ApiKey                  | SendGrid API key                  |                      |
| TinyUrl.BaseUrl                  | TinyURL API base URL              | https://api.tinyurl.com |
| TinyUrl.Domain                   | TinyURL API domain                |                      |
| TinyUrl.Token                    | TinyURL API token                 |                      |
| Urway.BaseUrl                    | Urway base URL                    | https://payments.urway-tech.com, https://payments-dev.urway-tech.com (dev) |
| Urway.TerminalId                 | Urway terminal ID                 |                      |
| Urway.Password                   | Urway password                    |                      |
| Urway.SecretKey                  | Urway secret key                  |                      |
| Urway.ApiAuthKey                 | Urway API authorization key       |                      |
| Urway.ApiAuthValue               | Urway API authorization value     |                      |
| Urway.CallbackPath               | Urway callback path               | /v1/callback/orders/pay |
| WebClient.BaseUrl                | Web client base URL               | https://www.kanzway.com, https://dev.kanzway.com (dev) |
| VendorClient.BaseUrl             | Vendor client base URL            | https://vendor.kanzway.com, https://vendor-dev.kanzway.com (dev) |
| AdminClient.BaseUrl              | Admin client base URL             | https://admin.kanzway.com, https://admin-dev.kanzway.com (dev) |

## Using User Secrets

### Init
```
dotnet user-secrets init
```

### Set a secret
Use colon (`:`) as key separator, example:
```
dotnet user-secrets set "KanzApi:Db:ConnectionString" "Server=localhost,1433;Database=MyDb;User=user;Password=password;Encrypt=False"
```
For array, use key separator with number after key, example:
```
dotnet user-secrets set "KanzApi:CorsPolicyOrigins:0" "https://kanzway.com"
```

## Using Environment Variables
Use double underscore (`__`) as key separator, example:
```
KanzApi__Db__ConnectionString="Server=localhost,1433;Database=MyDb;User=user;Password=password;Encrypt=False"
```
For array, use key separator with number after key, example:
```
KanzApi__CorsPolicyOrigins__0="https://kanzway.com"
```

# Running On IIS

## Deployment
Build for release:
```
dotnet publish --configuration Release
```
Then copy from:
```
<app_root>/bin/Release/net8.0/publish
```
To:
```
<root>\inetpub\wwwroot\<project_dir>
```

## Using User Secrets
Set secrets file here:
```
%WINDIR%\System32\config\systemprofile\AppData\Roaming\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
```

## Development Mode
Set environment variable:
```
ASPNETCORE_ENVIRONMENT=Development
```
Restart server:
```
net stop was /y
net start w3svc
```
