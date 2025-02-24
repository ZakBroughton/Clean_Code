using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class InitialiseDatabaseCommand : UI_Command
    {
        private readonly IDataGatewayFacade dataGatewayFacade;

        public InitialiseDatabaseCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            dataGatewayFacade.InitialiseMySqlDatabase();
            await dataGatewayFacade.AddEmployee(new Employee("Graham"));
            await dataGatewayFacade.AddEmployee(new Employee("Phil"));
            await dataGatewayFacade.AddEmployee(new Employee("Jan"));

            Item i1 = new Item("Pencil", 0.25f, 10, DateTime.Now);
            int i1Id = await dataGatewayFacade.AddItem(i1);
            dataGatewayFacade.AddTransactionLog(new TransactionDTO("Item Added", i1Id, i1.ItemName, 0.25f, i1.Quantity, "Graham", DateTime.Now));

            Item i2 = new Item("Eraser", 0.15f, 20, DateTime.Now);
            int i2Id = await dataGatewayFacade.AddItem(i2);
            dataGatewayFacade.AddTransactionLog(new TransactionDTO("Item Added", i2Id, i2.ItemName, 0.15f, i2.Quantity, "Phil", DateTime.Now));

            await dataGatewayFacade.RemoveQuantity(2, 4);
            dataGatewayFacade.AddTransactionLog(new TransactionDTO("Quantity Removed", i2Id, i2.ItemName, -0.1f, 4, "Graham", DateTime.Now));

            await dataGatewayFacade.AddQuantity(2, 2);
            dataGatewayFacade.AddTransactionLog(new TransactionDTO("Quantity Added", i2Id, i2.ItemName, 0.30f, 2, "Phil", DateTime.Now));
        }
    }
}