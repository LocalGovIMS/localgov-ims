# Accessible Autocomplete

The LocalGov IMS has been developed to be [WCAG 2.1 AA](https://www.w3.org/TR/WCAG21/) compliant.

A particularly tricky aspect of this was making dropdown controls accessible.
HTML select elements do not produce accessible code by default, so an alternative was required.
We researched many options and selected a control created by the [Government Digital Service](https://gds.blog.gov.uk/) called 'Accessible autocomplete'.

You can find the code for it [here](https://github.com/alphagov/accessible-autocomplete)

## Limitations

Whilst the control did almost everything we needed, it was limited in some of it's functionality.
The biggest issue was the ability to programmatically select an option, e.g. in scenarios that use cascading dropdowns.

A change which would facilitate this has been requested via an [issue](https://github.com/alphagov/accessible-autocomplete/issues/390) on the GitHub repository.
However, no work has been undertaken to action this.

## Resolution

A resolution was supplied in the [issue](https://github.com/alphagov/accessible-autocomplete/issues/390) by a third party.
It linked to a [forked repository](https://github.com/walkermatt/accessible-autocomplete) and this is the version that the LocalGov IMS uses.
