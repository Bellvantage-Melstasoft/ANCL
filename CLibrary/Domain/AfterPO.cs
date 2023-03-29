using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;


namespace CLibrary.Domain
{
    public class AfterPO
    {
        [DBField("ID")]
        public string ID { get; set; }

        [DBField("HYPER_LOAN")]
        public string HyperLoan { get; set; }

        [DBField("LC_OPENING")]
        public string LC_Opening { get; set; }

        [DBField("LDS")]
        public string LDS { get; set; }

        [DBField("EXPIRY")]
        public string Expiry { get; set; }

        [DBField("ETD")]
        public string ETD { get; set; }

        [DBField("ETA")]
        public string ETA { get; set; }


        [DBField("VESSEL")]
        public string Vessel { get; set; }

        [DBField("SHIPPING_AGENT")]
        public string ShippingAgent { get; set; }

        [DBField("INSURANCE_COMPANY")]
        public string InsuranceCompany { get; set; }
        [DBField("INSURANCE_DATE")]
        public string InsuranceDate { get; set; }
        [DBField("INSURANCE_POLICY_NO")]
        public string PolicyNo { get; set; }

        [DBField("PERFORMANCE_BOND_NO")]
        public string PerformanceBondNo { get; set; }

        [DBField("PERFORMANCE_DATE")]
        public string PerformanceDate { get; set; }

        [DBField("CHARGE_VALUE")]
        public double ChargeValue { get; set; }

        [DBField("INSURANCE_CHARGE")]
        public double InsuranceCharge { get; set; }

        [DBField("FREIGHT")]
        public double Freight { get; set; }


        [DBField("DUTY")]
        public double Duty { get; set; }

        [DBField("CUSTOM_DUTY")]
        public double CustomDuty { get; set; }

        [DBField("CESS")]
        public double CESS { get; set; }

        [DBField("PAL")]
        public double PAL { get; set; }

        [DBField("VAT")]
        public double VAT { get; set; }

        [DBField("NBT")]
        public double NBT { get; set; }

        [DBField("OTHER_CUSTOM_CHARGES")]
        public double OtherCustomCharges { get; set; }

        [DBField("RANK_CONTAINER")]
        public double RANK_CONTAINER { get; set; }

        [DBField("TERMINAL")]
        public double TERMINAL { get; set; }

        [DBField("WEIGHING_CHARGES")]
        public double WeighingCharges { get; set; }

        [DBField("SLPA")]
        public double SLPA { get; set; }

        [DBField("AGENT_DO_CHARGE")]
        public double AgentDoCharges { get; set; }

        [DBField("CONTAINER_DEPOSIT")]
        public double ContainerDeposit { get; set; }

        [DBField("WASHING_CHARGES")]
        public double WashingCharges { get; set; }

        [DBField("CLEARING_AND_TRANSPORT_charges")]
        public double ClearingAndTransportCharges { get; set; }

    }
}




