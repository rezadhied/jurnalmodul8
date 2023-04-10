using System;
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

class BankTransferConfig
{
    public string lang { get; set; }
    public Transfer transfer { get; set; }
    
    public List<string> methods { get; set; }

    public BankTransferConfig() 
    {
        lang = "id";
        List<string> methods = new List<string>(){ "RTO (real-time)", "SKN", "RTGS", "BI FAST" };
        
    }

    public void ReadJSON()
    {
        try
        {
            string jsonString = File.ReadAllText("bank_transfer_config.json");
            BankTransferConfig config = JsonConvert.DeserializeObject<BankTransferConfig>(jsonString);
            lang = config.lang;
            methods = config.methods;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to read configuration file: " + ex.Message);
        }
    }

    public void WriteJSON()
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText("bank_transfer_config.json", jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to write configuration file: " + ex.Message);
        }
    }
}

class Transfer
{
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }

    public Transfer()
    {
        threshold = 25000000;
        low_fee = 6500;
        high_fee = 15000;
    }

    public void ReadJSON()
    {
        try
        {
            string jsonString = File.ReadAllText("bank_transfer_config.json");
            Transfer config = JsonConvert.DeserializeObject<Transfer>(jsonString);
            threshold = config.threshold;
            low_fee = config.low_fee;
            high_fee = config.high_fee;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to read configuration file: " + ex.Message);
        }
    }

    public void WriteJSON()
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText("bank_transfer_config.json", jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to write configuration file: " + ex.Message);
        }
    }
}

class Confirmation
{

    public string en { get; set; }
    public string id { get; set; }

    public Confirmation()
    {
        en = "yes";
        id = "ya";
    }

    public void ReadJSON()
    {
        try
        {
            string jsonString = File.ReadAllText("bank_transfer_config.json");
            Confirmation config = JsonConvert.DeserializeObject<Confirmation>(jsonString);
            en = config.en;
            id = config.id;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to read configuration file: " + ex.Message);
        }
    }

    public void WriteJSON()
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText("bank_transfer_config.json", jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to write configuration file: " + ex.Message);
        }
    }
}

class program
{
    static void Main(string[] args)
    {
        BankTransferConfig config = new BankTransferConfig();
        config.ReadJSON();
        Transfer transfer = new Transfer();
        transfer.ReadJSON();
        Confirmation confirmation = new Confirmation();
        confirmation.ReadJSON();

        if (config.lang == "en")
        {
            Console.WriteLine("Please insert the amount of money to transfer:");
        }
        else
        {
            Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:");
        }

        int uang, biaya = 0, total;
        int.TryParse(Console.ReadLine(), out uang);

        if (uang <= transfer.threshold)
        {
            biaya = transfer.low_fee;
        }else if (uang > transfer.threshold)
        {
            biaya = transfer.high_fee;
        }

        total = uang + biaya;

        if (config.lang != "en")
        {
            Console.WriteLine("Transfer fee = " + biaya);
            Console.WriteLine("Total amount = "+total);
        }
        else
        {
            Console.WriteLine("Biaya transfer = "+ biaya);
            Console.WriteLine("Total biaya = " + total);
        }

        if (config.lang == "en")
        {
            Console.WriteLine("Select transfer method");
        }
        else
        {
            Console.WriteLine("Pilih metode transfer");
        }

        /*for (int i = 0; i < config.methods.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + config.methods[i]);
        }*/

        Console.WriteLine("1. RTO (real-time)");
        Console.WriteLine("2. SKN");
        Console.WriteLine("3. RTGS");
        Console.WriteLine("4. BI FAST");

        if ( config.lang == "en")
        {
            Console.WriteLine("Please type '"+confirmation.en+"' to confirm the transaction:");
        }
        else
        {
            Console.WriteLine("Ketik '" + confirmation.id + "' untuk mengkonfirmasi transaksi");
        }
        string input;
        input = Console.ReadLine();
        if (input == confirmation.en || input == confirmation.id)
        {
            if (config.lang == "en")
            {
                Console.WriteLine("The transfer is completed");
            }
            else
            {
                Console.WriteLine("Proses transfer berhasil");
            }
        }
        else
        {
            if (config.lang.Equals("en"))
            {
                Console.WriteLine("Transfer is cancelled");
            }
            else
            {
                Console.WriteLine("Transfer dibatalkan");
            }
        }
    }
}