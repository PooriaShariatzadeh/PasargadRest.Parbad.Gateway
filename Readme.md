```
---------------------------------------------------
Parbad - Online Payment Library for .NET developers
				Pasargad Rest Gateway
---------------------------------------------------

GitHub: https://github.com/PooriaShariatzadeh/PasargadRest.Parbad.Gateway
Tutorials: https://github.com/Sina-Soltani/Parbad/wiki

-------------
Configuration
-------------

.ConfigureGateways(gateways =>
{
    gateways
        .AddPasargadRest()
        .WithAccounts(accounts =>
        {
            accounts.AddInMemory(account =>
            {
                account.TerminalCode = <Your ID>;
                account.Usernamne = "<Your UserName>";
                account.Password = "<Your Password>";
            });
        });
})

-------------
Making a request
-------------

var result = _onlinePayment.RequestAsync(invoice => 
{
    invoice
	.UsePasargadRest()

})

-------------
Getting the original verification result
-------------
var result = _onlinePayment.VerifyAsync(invoice);
```
