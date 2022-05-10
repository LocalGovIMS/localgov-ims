# Fund Metadata 

## Metadata Keys

The following Fund metadata keys are definied in the system:

|Key|Example Value(s)|Description|
|---|-------------|-----------|
|IsACreditNoteEnabledFund|TRUE, FALSE|Determines whether the fund can have credit notes generated for it. You can set this to 'TRUE' for many funds.|
|IsAnEReturnDefaultFund|TRUE, FALSE|Determines whether the fund is the default fund used for EReturns. This should only be set to 'TRUE' for one fund.|
|IsASuspenseJournalFund|TRUE, FALSE|Determines whether this fund is used when journalling suspense items. This should only be set to 'TRUE' for one fund.|
|IsABasketFund|TRUE, FALSE|Determines whether this fund will show in the Payment Portal 'Payment type' dropdown list|
|Basket.ReferenceFieldLabel||The text that will be shown for this fund in the Payment Portal 'Payment type' dropdown list.|
|Basket.ReferenceFieldMessage||Any additional text that you want to display for this fund to describe the reference field in more detail within the Payment Portal once this find has been selected in the 'Payment type' dropdown list.|
