# App Settings

## Payment Portal

### General Settings

|Key|Example Value|Description|
|---|-------------|-----------|
|PortalPaymentsURL||Your Payment Portal URL here|
|FinishURL||The URL you want to see at the end of the payment process|
|ApplicationEventLogSourceName|PaymentPortal|TODO: Is this even needed?|
|Layout.Strapline||A strap line for your organisation|
|OtherWaysToPayUrl||A link to a list of other ways to pay|
|ReferenceSalt||The salt used when creating transaction references|

### Email

|Key|Example Value|Description|
|---|-------------|-----------|
|EmailMessageFrom|noreply@your-organisation.com|The email that all emails sent from the the system will appear to have been sent from|
|EmailMessageFromDisplayName||Your organisation's name|
|EmailHost|emailhost.your-organisation.com|The address of your email host|
|Notification.Email.Recipient|webmaster@your-organisation.com|The email address for the recipient of any system notifications|
|DuplicateTransaction.Email.Recipient|webmaster@your-organisation.com|The email address for the recipient of the duplicate transaction alert email|

### Organisation

|Key|Example Value|Description|
|---|-------------|-----------|
|Organisation.Name||Your organisation name|
|Organisation.ShortName||Your organisation short name. If you don't have one, make this your full name|
|Organisation.AbbreviatedName||Your organisation abbreviation. If you don't have one, make this your full name|
|Organisation.Website|https://www.your-organisation.com|Your organsaitions website|
|Organisation.Logo|https://www.your-organisaition.com/logo.png|Your organisations logo|
|Organisation.Logo.Printable|https://www.your-organisaition.com/print-logo.png|A printable version of your logo|
|Organisation.VatNumber|123456789|Your organisations VAT number|
|Organisation.VatRegisteredAddress.AddressLine1|VAT Address Line 1|First line of your orgsanisations VAT address|
|Organisation.VatRegisteredAddress.AddressLine2|VAT Address Line 2|Second line of your orgainsations VAT address|
|Organisation.VatRegisteredAddress.AddressLine3|VAT Address Line 3|Third line of your organisations VAT address|
|Organisation.VatRegisteredAddress.PostCode||VAT Address Post Code|
|Organisation.Address.AddressLine1|Address Line 1|First line of your orgsanisations address|
|Organisation.Address.AddressLine2|Address Line 2|Second line of your orgainsations address|
|Organisation.Address.AddressLine3|Address Line 3|Third line of your organisations address|
|Organisation.Address.PostCode||Post Code|

### Demo Seed Data

|Key|Example Value|Description|
|---|-------------|-----------|
|SeedData.DemoData.PaymentIntegration.Name|Integration||
|SeedData.DemoData.PaymentIntegration.BaseUri|https://www.your-payment-integration.com/Payment||
|SeedData.DemoData.User1.Username|tester1@your-organisation.com||
|SeedData.DemoData.User1.Name|Tester 1||
|SeedData.DemoData.User1.PasswordHash|AP59Z8CoHL2gGqqgLb/CTu0u2sbCkw+zkSxqA1lh+MmGRkqvORuRRFG2lHMdmsqbTg==||
|SeedData.DemoData.User2.Username|tester2@your-organisation.com||
|SeedData.DemoData.User2.Name|Tester 2||
|SeedData.DemoData.User2.PasswordHash|AP59Z8CoHL2gGqqgLb/CTu0u2sbCkw+zkSxqA1lh+MmGRkqvORuRRFG2lHMdmsqbTg==||
|SeedData.DemoData.FundMetadata.Key1|IsABasketFund||
|SeedData.DemoData.FundMetadata.Value1|TRUE||
|SeedData.DemoData.FundMetadata.FundCode1|F1||

## Admin

### General Settings

|Key|Example Value|Description|
|---|-------------|-----------|
|ReferenceSalt||The salt used when creating transaction references|
|FileImport.UploadDirectory|~/Uploads/|The directory that any imports files which are uploaded will be saved to|

### Email

|Key|Example Value|Description|
|---|-------------|-----------|
|EmailMessageTo|noreply@your-organisation.com|?|
|EmailMessageFrom|noreply@your-organisation.com|The email that all emails sent from the the system will appear to have been sent from|
|EmailMessageFromDisplayName||Your organisation's name|
|EmailHost|emailhost.your-organisation.com|The address of your email host|
|EReturnDeletion.Email.Recipient|webmaster@your-organisation.com|The email address for the recipient of notifications for when an eReturn is deleted|
|Notification.Email.Recipient|webmaster@your-organisation.com|The email address for the recipient of any system notifications|
|DuplicateTransaction.Email.Recipient|webmaster@your-organisation.com|The email address for the recipient of the duplicate transaction alert email|

### Organisation

|Key|Example Value|Description|
|---|-------------|-----------|
|Organisation.Name||Your organisation name|
|Organisation.ShortName||Your organisation short name. If you don't have one, make this your full name|
|Organisation.AbbreviatedName||Your organisation abbreviation. If you don't have one, make this your full name|
|Organisation.Website|https://www.your-organisation.com|Your organsaitions website|
|Organisation.Logo|https://www.your-organisaition.com/logo.png|Your organisations logo|
|Organisation.Logo.Printable|https://www.your-organisaition.com/print-logo.png|A printable version of your logo|
|Organisation.VatNumber|123456789|Your organisations VAT number|
|Organisation.VatRegisteredAddress.AddressLine1|VAT Address Line 1|First line of your orgsanisations VAT address|
|Organisation.VatRegisteredAddress.AddressLine2|VAT Address Line 2|Second line of your orgainsations VAT address|
|Organisation.VatRegisteredAddress.AddressLine3|VAT Address Line 3|Third line of your organisations VAT address|
|Organisation.VatRegisteredAddress.PostCode||VAT Address Post Code|
|Organisation.Address.AddressLine1|Address Line 1|First line of your orgsanisations address|
|Organisation.Address.AddressLine2|Address Line 2|Second line of your orgainsations address|
|Organisation.Address.AddressLine3|Address Line 3|Third line of your organisations address|
|Organisation.Address.PostCode||Post Code|

## API

### General Settings

|Key|Example Value|Description|
|---|-------------|-----------|
|ReferenceSalt||The salt used when creating transaction references|
