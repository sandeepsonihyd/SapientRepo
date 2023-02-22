using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
//string clientId = "4cfd356c-08a8-4835-b0cd-e662787e2164";
//string tenantID = "82d8af3b-d3f9-465c-b724-0fb186cc28c7";
//string clientSecret = "aoG8Q~p~yRqcNSogn0D5oVeBpbO5jfC9lC_Ukavh";
string keyVaultUrl = "https://dssdemo-keyvault.vault.azure.net";
string secretName = "S1";
//var credential = new ClientSecretCredential(tenantID, clientId, clientSecret);
var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
// Retrieve the secret
KeyVaultSecret sec = client.GetSecret(secretName);
// Print the secret value to the console
Console.WriteLine(sec.Value);




