using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Helpers
{
    public static class PaymentTypes
    {
        public const string AccountMoney = "account_money";
        public const string Ticket = "ticket";
        public const string BankTransfer = "bank_transfer";
        public const string Atm = "atm";
        public const string CreditCard = "credit_card";
        public const string DebitCard = "debit_card";
        public const string PrepaidCard = "prepaid_card";
        public const string DigitalCurrency = "digital_currency";
        public const string VoucherCard = "voucher_card";
        public const string CryptoTransfer = "crypto_transfer";
    }
}
