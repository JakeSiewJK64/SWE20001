using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Enums
{
    public enum Status
    {
        Full = 0,
        OutOfStock = 1,
        Stale = 2,
        SellingFast = 3,
        LowStock = 4,
        Normal = 5
    }

    public enum ItemCategory
    {
        Narcotics = 0,
        Painkillers = 1,
        Sedatives = 2,
        Hallucinogens = 3,
        Stimulants = 4,
        Inhalants = 5,
        Vitammins = 6,
        AntiHistamine = 7,
        AntiEpileptic = 8,
        Amphetamines = 9,
        Depressants = 10,
        Opiates = 11,
        Antacids = 12, 
        Antiviral = 13,
        Antibacterial = 14,
        Anesthetics = 15,
        Analgesics = 16,
        Bandages = 17,
        SurgeryGradeBandages = 18,
        Supplements = 19,
        SkinCare = 20,
        BirthControl = 21
    }
}
