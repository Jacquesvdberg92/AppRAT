import random
from datetime import datetime, timedelta

franchises = ['Waranda', 'AnotherFranchise', 'YetAnotherFranchise']
sales_people = [2,3,4,5]
clients = ['Client1', 'Client2', 'Client3', 'Client4', 'Client5']
results_ids = [2, 3, 4, 5, 6, 7]
condition_ids = [2, 3, 4]
insurance_ids = [2, 3, 4]
remarks_ids = [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19]
dealer_ids = [1]

# Generate random data
num_records = 600  # Change this to the desired number of records

with open('generated_queries.sql', 'w') as file:
    for _ in range(num_records):
        franchise = random.choice(franchises)
        sales_person = random.choice(sales_people)
        client = random.choice(clients)
        date = (datetime.now() - timedelta(days=random.randint(1, 365))).strftime('%Y-%m-%d')
        results_id = random.choice(results_ids)
        condition_id = random.choice(condition_ids)
        validated = random.randint(0, 1)
        invoiced = random.randint(0, 1)
        signed = random.randint(0, 1)
        insurance_id = random.choice(insurance_ids)
        trade_in = random.randint(0, 1)
        paid = random.randint(0, 1)
        spotter = random.randint(0, 1)
        client_out_of_town = random.randint(0, 1)
        remarks_id = random.choice(remarks_ids)
        comments = 'Random comments'
        dealer_id = random.choice(dealer_ids)

        sql_statement = (
            f"INSERT INTO [AppRat].[dbo].[AR_Applications]([Franchise],[UserId],[SalesPeople],[Client],[Date],[Results_Id],[Condition_Id],[Validated],[Invoiced],[Signed],[Insurance_Id],[TradeIn],[Paid],[Spotter],[ClientOutOfTown],[Remarks_Id],[Comments],[DealerId])"
            f"VALUES('{franchise}','TestUser','{sales_person}','{client}','{date}',{results_id},{condition_id},{validated},{invoiced},{signed},{insurance_id},{trade_in},{paid},{spotter},{client_out_of_town},{remarks_id},'{comments}',{dealer_id})"
        )

        # Write the SQL statement to the file
        file.write(sql_statement + '\n')

print("SQL statements generated and saved to 'generated_queries.sql'")
