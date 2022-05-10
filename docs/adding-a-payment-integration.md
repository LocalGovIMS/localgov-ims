# Adding a Payment Integration

The Payment Portal uses 3rd party payment services to process payments.
These payment services are provided through payment integrations.

To be able to take a payment through the Payment Portal or Admin you will either need to configure and run one of the existing payment integrations, or write a new one.

There are two aspects to configuring a payment integration. 
1. Configuring the integration itself (see the readme of the integration(s) you are using)
2. Configuring a Payment Integration within the IMS.

## Setting Up a Payment Integration

Part 1: Setting up the payment integration
1. Run the Admin application
2. Navigate to Settings > Payment Integrations
3. Click 'Create payment integrations'
4. Enter the payment integration details and click Save
5. Once returned to the payment integration list screen, not the ID of the payment integration you just created.

Part 2: Configuring the MOPs you want to use the payment integration
1. Navigate to Settings > Methods of Payment (MOP)
2. For each of the MOPs add a metadata entry 'PaymentIntegrationId' with the ID of the record you just created.

Part 3: Any custom configuration
See the documentation for the specific payment integration to see if there is any further setup