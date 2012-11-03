MPower_DotNET
=============

MPower Payments .NET Client Library

## Installation

Add Assembly file Dependencies from the `bin/Release` directory

    MPowerPayments.dll
    Newtonsoft.Json.dll

## Setup your API Keys

    MPowerSetup setup = new MPowerSetup {
      MasterKey = "82403450-ee3a-4c58-9564-a8fbe30c5fb7",
      PrivateKey = "test_private_jKxSyaxlcQdrQcuxAOFAbxvK5w4",
      PublicKey = "test_public_M6-fRS1RxnzlGqgeLaBF5vLLoKs",
      Token = "7f6c81c1ea223174416e",
      Mode = "test"
    };

## Setup your checkout store information

    MPowerStore store = new MPowerStore {
      Name = "DotNet Online Store",
      Tagline = "This is my awesome tagline",
      PhoneNumber = "0244000001",
      PostalAddress = "P. O. Box 10770 Accra North Ghana"
    };

Customer will be redirected back to this URL when he cancels the checkout on MPower website

    store.CancelUrl = "CHECKOUT_CANCEL_URL";

MPower will automatically redirect customer to this URL after successfull payment.
This setup is optional and highly recommended you dont set it, unless you want to customize the payment experience of your customers.
When a returnURL is not set, MPower will redirect the customer to the receipt page.

    store.ReturnUrl = "CHECKOUT_RETURN_URL";

## Create your Checkout Invoice
Please note that MPowerCheckoutInvoice Class requires two parameters which should be instances of MPowerSetup & MPowerStore respectively

    MPowerCheckoutInvoice co = new MPowerCheckoutInvoice (setup, store);

Params for addItem function `AddItem(name_of_item,quantity,unit_price,total_price,[description])`

      co.AddItem ("A Big Bag of Rice", 1, 20.99, 41.89);
      co.AddItem ("Huge Bag of Chocolates", 1, 20.99, 41.89);
      co.AddItem ("Pair of trousers", 1, 20.99, 41.89);

## Set the total amount to be charged ! Important

    co.SetTotalAmount (100.50);

## Setup Tax Information (Optional)

    co.AddTax("VAT (15)",50);
    co.AddTax("NHIL (10)",50);

## You can add custom data to your invoice which can be called back later

    co.SetCustomData("Firstname","Alfred");
    co.SetCustomData("Lastname","Rowe");
    co.SetCustomData("CartId",929292872);

## Pushing invoice to MPower server and getting your URL

    if(co.Create()) {
      Console.WriteLine (co.ResponseText);
      Console.WriteLine ("Invoice URL: "+co.GetInvoiceUrl());
    }else{
      Console.WriteLine ("Error Message: "+co.ResponseText);
    }

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request