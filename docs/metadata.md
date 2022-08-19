# Metadata

We use metadata to attach meaning and capabilities to our entities without bloating the entity itself.
Metadata is stored using a key/value structure 

# Types of Metadata

There are two types of metadata:

1. System
2. Custom

## System Metadata

This is metadata that either the Admin or Payment Portal applications need to function effectively.
Examples are: BackgroundColour - allowing the user to specify a background colour where a Method of Payment is displayed.

Various entities have system metadata, such as:

* [Fund Code Metadata](metadata-fund.md)
* [Method of Payment Metadata](metadata-method-of-payment.md)
* [VAT Code Metadata](metadata-vat.md)

## Custom Metadata

This is metadata which can be created by the user to attach new meaning and capabilities.
At present there is no UI for this.
An example of this would be adding a new key/value to describe the export functionality of a Fund - such as the SAP general ledger code it relates to.