using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using Microsoft.Azure.Management.Compute.Fluent.Models;

public class AzureVirtualMachineExample
{
    public static async Task CreateVirtualMachineAsync()
    {
        // Authenticate with Azure
        var credential = new DefaultAzureCredential();
        var subscriptionId = "<YourSubscriptionId>";
        var resourceGroupName = "MyResourceGroup";
        var vmName = "MyVirtualMachine";
        var location = "EastUS";

        // Initialize the Azure Resource Manager Client
        var armClient = new ArmClient(credential, subscriptionId);

        // Create a resource group if it doesn't exist
        var resourceGroup = await armClient.GetResourceGroupResource(new ResourceIdentifier($"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}"))
                                           .GetOrCreateAsync(new ResourceGroupData(location));

        // Define the virtual machine parameters
        var vmData = new VirtualMachineData(location)
        {
            HardwareProfile = new HardwareProfile()
            {
                VmSize = VirtualMachineSizeTypes.StandardD2sV3
            },
            OSProfile = new OSProfile()
            {
                ComputerName = vmName,
                AdminUsername = "azureuser",
                AdminPassword = "<YourPassword>"  // Use secrets in production
            },
            NetworkProfile = new NetworkProfile()
            {
                NetworkInterfaces = { new NetworkInterfaceReference() { Id = "<NetworkInterfaceId>" } }
            },
            StorageProfile = new StorageProfile()
            {
                ImageReference = new ImageReference()
                {
                    Publisher = "Canonical",
                    Offer = "UbuntuServer",
                    Sku = "18.04-LTS",
                    Version = "latest"
                },
                OsDisk = new OSDisk(DiskCreateOptionTypes.FromImage)
                {
                    Name = $"{vmName}_OsDisk",
                    Caching = CachingTypes.ReadWrite,
                    ManagedDisk = new ManagedDiskParameters() { StorageAccountType = StorageAccountTypes.StandardLRS }
                }
            }
        };

        // Create the virtual machine
        var vmLro = await resourceGroup.GetVirtualMachines().CreateOrUpdateAsync(WaitUntil.Completed, vmName, vmData);
        var virtualMachine = vmLro.Value;

        Console.WriteLine($"Virtual Machine '{virtualMachine.Data.Name}' created successfully.");
    }
}
