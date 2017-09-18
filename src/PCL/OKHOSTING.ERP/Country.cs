using System;
using OKHOSTING.Data.Validation;
using System.Collections.Generic;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A country
	/// </summary>
	public class Country
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Name of the country
		/// </summary>
		/// <example>México, France, Brasil</example>
		[StringLengthValidator(50)]
		[RequiredValidator]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Short abbreviation of the country in ISO 2 letter format.
		/// </summary>
		/// <example>MX, FR, BR</example>
		[StringLengthValidator(2)]
		public string Iso2
		{
			get;
			set;
		}

		/// <summary>
		/// Short abbreviation of the country in ISO 3 letter format
		/// </summary>
		/// <example>MEX, FRA, BRA</example>
		[StringLengthValidator(3)]
		public string Iso3
		{
			get;
			set;
		}

		/// <summary>
		/// Gets an international numeric unique identifier of the country
		/// </summary>
		/// <example>484 for México, 840 for USA</example>
		public int? NumericCode
		{
			get;
			set;
		}

		/// <summary>
		/// Phone area code of the country
		/// </summary>
		/// <example>52 for México, 01 for USA</example>
		public int? PhoneCode
		{
			get;
			set;
		}

		/// <summary>
		/// Main currency used in the country
		/// </summary>
		[RequiredValidator]
		public Currency Currency
		{
			get;
			set;
		}

		public Country()
		{
		}

		/// <summary>
		/// Used for populating initial data only
		/// </summary>
		public Country(string name, string iso2, string iso3, int? numericCode, int? phoneCode)
		{
			Name = name;
			Iso2 = iso2;
			Iso3 = iso3;
			NumericCode = numericCode;
			PhoneCode = phoneCode;
		}

		/// <summary>
		/// Returns the initial collection of countries that should be created on system setup
		/// </summary>
		public static IEnumerable<Country> GetSetupObjects()
		{
			yield return new Country("Afghanistan", "AF", "AFG", 4, 244);
			yield return new Country("Albania", "AL", "ALB", 8, 376);
			yield return new Country("Algeria", "DZ", "DZA", 12, 357);
			yield return new Country("American Samoa", "AS", "ASM", 16, 1340);
			yield return new Country("Andorra", "AD", "AND", 20, 377);
			yield return new Country("Angola", "AO", "AGO", 24, 1268);
			yield return new Country("Anguilla", "AI", "AIA", 660, 355);
			yield return new Country("Antarctica", "AQ", "ATA", null, 994);
			yield return new Country("Antigua and Barbuda", "AG", "ATG", 28, 1787);
			yield return new Country("Argentina", "AR", "ARG", 32, 1684);
			yield return new Country("Armenia", "AM", "ARM", 51, 373);
			yield return new Country("Aruba", "AW", "ABW", 533, 374);
			yield return new Country("Australia", "AU", "AUS", 36, 54);
			yield return new Country("Austria", "AT", "AUT", 40, 1264);
			yield return new Country("Azerbaijan", "AZ", "AZE", 31, 672);
			yield return new Country("Bahamas", "BS", "BHS", 44, 229);
			yield return new Country("Bahrain", "BH", "BHR", 48, 61);
			yield return new Country("Bangladesh", "BD", "BGD", 50, 225);
			yield return new Country("Barbados", "BB", "BRB", 52, 973);
			yield return new Country("Belarus", "BY", "BLR", 112, 598);
			yield return new Country("Belgium", "BE", "BEL", 56, 993);
			yield return new Country("Belize", "BZ", "BLZ", 84, 501);
			yield return new Country("Benin", "BJ", "BEN", 204, 32);
			yield return new Country("Bermuda", "BM", "BMU", 60, 1284);
			yield return new Country("Bhutan", "BT", "BTN", 64, 1242);
			yield return new Country("Bolivia", "BO", "BOL", 68, 880);
			yield return new Country("Bosnia and Herzegovina", "BA", "BIH", 70, 55);
			yield return new Country("Botswana", "BW", "BWA", 72, 1246);
			yield return new Country("Bouvet Island", "BV", "BVT", null, 387);
			yield return new Country("Brazil", "BR", "BRA", 76, 375);
			yield return new Country("British Indian Ocean Territory", "IO", "IOT", null, 39);
			yield return new Country("Brunei Darussalam", "BN", "BRN", 96, 1441);
			yield return new Country("Bulgaria", "BG", "BGR", 100, 267);
			yield return new Country("Burkina Faso", "BF", "BFA", 854, 43);
			yield return new Country("Burundi", "BI", "BDI", 108, 297);
			yield return new Country("Cambodia", "KH", "KHM", 116, 962);
			yield return new Country("Cameroon", "CM", "CMR", 120, 53);
			yield return new Country("Canada", "CA", "CAN", 124, 237);
			yield return new Country("Cape Verde", "CV", "CPV", 132, 243);
			yield return new Country("Cayman Islands", "KY", "CYM", 136, 254);
			yield return new Country("Central African Republic", "CF", "CAF", 140, 27);
			yield return new Country("Chad", "TD", "TCD", 148, 238);
			yield return new Country("Chile", "CL", "CHL", 152, 61);
			yield return new Country("China", "CN", "CHN", 156, 57);
			yield return new Country("Christmas Island", "CX", "CXR", 61, 678);
			yield return new Country("Cocos (Keeling) Islands", "CC", "CCK", 61891, 226);
			yield return new Country("Colombia", "CO", "COL", 170, 56);
			yield return new Country("Comoros", "KM", "COM", 174, 241);
			yield return new Country("Congo", "CG", "COG", 178, 1345);
			yield return new Country("Congo, the Democratic Republic of the", "CD", "COD", 180, 58);
			yield return new Country("Cook Islands", "CK", "COK", 184, 359);
			yield return new Country("Costa Rica", "CR", "CRI", 188, 86);
			yield return new Country("Cote D'Ivoire", "CI", "CIV", 384, 91);
			yield return new Country("Croatia", "HR", "HRV", 191, 242);
			yield return new Country("Cuba", "CU", "CUB", 192, 269);
			yield return new Country("Cyprus", "CY", "CYP", 196, 855);
			yield return new Country("Czech Republic", "CZ", "CZE", 203, 682);
			yield return new Country("Denmark", "DK", "DNK", 208, 298);
			yield return new Country("Djibouti", "DJ", "DJI", 262, 594);
			yield return new Country("Dominica", "DM", "DMA", 212, 53);
			yield return new Country("Dominican Republic", "DO", "DOM", 214, 385);
			yield return new Country("Ecuador", "EC", "ECU", 218, 213);
			yield return new Country("Egypt", "EG", "EGY", 818, 690);
			yield return new Country("El Salvador", "SV", "SLV", 222, 677);
			yield return new Country("Equatorial Guinea", "GQ", "GNQ", 226, 220);
			yield return new Country("Eritrea", "ER", "ERI", 232, 232);
			yield return new Country("Estonia", "EE", "EST", 233, 420);
			yield return new Country("Ethiopia", "ET", "ETH", 231, 593);
			yield return new Country("Falkland Islands (Malvinas)", "FK", "FLK", 238, 240);
			yield return new Country("Faroe Islands", "FO", "FRO", 234, 372);
			yield return new Country("Fiji", "FJ", "FJI", 242, 291);
			yield return new Country("Finland", "FI", "FIN", 246, 1767);
			yield return new Country("France", "FR", "FRA", 250, 20);
			yield return new Country("French Guiana", "GF", "GUF", 254, 689);
			yield return new Country("French Polynesia", "PF", "PYF", 258, 47);
			yield return new Country("French Southern Territories", "TF", "TFS", null, 84);
			yield return new Country("Gabon", "GA", "GAB", 266, 687);
			yield return new Country("Gambia", "GM", "GMB", 270, 33);
			yield return new Country("Georgia", "GE", "GEO", 268, 358);
			yield return new Country("Germany", "DE", "DEU", 276, 506);
			yield return new Country("Ghana", "GH", "GHA", 288, null);
			yield return new Country("Gibraltar", "GI", "GIB", 292, 299);
			yield return new Country("Greece", "GR", "GRC", 300, 49);
			yield return new Country("Greenland", "GL", "GRL", 304, 233);
			yield return new Country("Grenada", "GD", "GRD", 308, 676);
			yield return new Country("Guadeloupe", "GP", "GLP", 312, 253);
			yield return new Country("Guam", "GU", "GUM", 316, 251);
			yield return new Country("Guatemala", "GT", "GTM", 320, 30);
			yield return new Country("Guinea", "GN", "GIN", 324, 970);
			yield return new Country("Guinea-Bissau", "GW", "GNB", 624, 350);
			yield return new Country("Guyana", "GY", "GUY", 328, 1473);
			yield return new Country("Haiti", "HT", "HTI", 332, 590);
			yield return new Country("Heard Island and Mcdonald Islands", "HM", "HMI", null, 1284);
			yield return new Country("Holy See (Vatican City State)", "VA", "VAT", 336, 256);
			yield return new Country("Honduras", "HN", "HND", 340, 502);
			yield return new Country("Hong Kong", "HK", "HKG", 344, 224);
			yield return new Country("Hungary", "HU", "HUN", 348, 245);
			yield return new Country("Iceland", "IS", "ISL", 352, 36);
			yield return new Country("India", "IN", "IND", 356, 852);
			yield return new Country("Indonesia", "ID", "IDN", 360, null);
			yield return new Country("Iran, Islamic Republic of", "IR", "IRN", 364, 592);
			yield return new Country("Iraq", "IQ", "IRQ", 368, 504);
			yield return new Country("Ireland", "IE", "IRL", 372, 509);
			yield return new Country("Israel", "IL", "ISR", 376, 998);
			yield return new Country("Italy", "IT", "ITA", 380, 354);
			yield return new Country("Jamaica", "JM", "JAM", 388, 62);
			yield return new Country("Japan", "JP", "JPN", 392, 98);
			yield return new Country("Jordan", "JO", "JOR", 400, 353);
			yield return new Country("Kazakhstan", "KZ", "KAZ", 398, 82);
			yield return new Country("Kenya", "KE", "KEN", 404, 964);
			yield return new Country("Kiribati", "KI", "KIR", 296, 673);
			yield return new Country("Korea, Democratic People's Republic of", "KP", "PRK", 408, 1876);
			yield return new Country("Korea, Republic of", "KR", "KOR", 410, 81);
			yield return new Country("Kuwait", "KW", "KWT", 414, 7);
			yield return new Country("Kyrgyzstan", "KG", "KGZ", 417, 972);
			yield return new Country("Lao People's Democratic Republic", "LA", "LAO", 418, 965);
			yield return new Country("Latvia", "LV", "LVA", 428, 692);
			yield return new Country("Lebanon", "LB", "LBN", 422, 974);
			yield return new Country("Lesotho", "LS", "LSO", 426, 371);
			yield return new Country("Liberia", "LR", "LBR", 430, 856);
			yield return new Country("Libyan Arab Jamahiriya", "LY", "LBY", 434, 60);
			yield return new Country("Liechtenstein", "LI", "LIE", 438, 1);
			yield return new Country("Lithuania", "LT", "LTU", 440, 850);
			yield return new Country("Luxembourg", "LU", "LUX", 442, 961);
			yield return new Country("Macao", "MO", "MAC", 446, 960);
			yield return new Country("Macedonia, the Former Yugoslav Republic of", "MK", "MKD", 807, 228);
			yield return new Country("Madagascar", "MG", "MDG", 450, 265);
			yield return new Country("Malawi", "MW", "MWI", 454, 218);
			yield return new Country("Malaysia", "MY", "MYS", 458, 230);
			yield return new Country("Maldives", "MV", "MDV", 462, 231);
			yield return new Country("Mali", "ML", "MLI", 466, 370);
			yield return new Country("Malta", "MT", "MLT", 470, 853);
			yield return new Country("Marshall Islands", "MH", "MHL", 584, 64);
			yield return new Country("Martinique", "MQ", "MTQ", 474, 596);
			yield return new Country("Mauritania", "MR", "MRT", 478, 389);
			yield return new Country("Mauritius", "MU", "MUS", 480, 261);
			yield return new Country("Mayotte", "YT", "MYT", null, 381);
			yield return new Country("Mexico", "MX", "MEX", 484, 1340);
			yield return new Country("Micronesia, Federated States of", "FM", "FSM", 583, 503);
			yield return new Country("Moldova, Republic of", "MD", "MDA", 498, 423);
			yield return new Country("Monaco", "MC", "MCO", 492, 266);
			yield return new Country("Mongolia", "MN", "MNG", 496, null);
			yield return new Country("Montserrat", "MS", "MSR", 500, 356);
			yield return new Country("Morocco", "MA", "MAR", 504, 223);
			yield return new Country("Mozambique", "MZ", "MOZ", 508, 976);
			yield return new Country("Myanmar", "MM", "MMR", 104, 352);
			yield return new Country("Namibia", "NA", "NAM", 516, 95);
			yield return new Country("Nauru", "NR", "NRU", 520, 212);
			yield return new Country("Nepal", "NP", "NPL", 524, 269);
			yield return new Country("Netherlands", "NL", "NLD", 528, 31);
			yield return new Country("Netherlands Antilles", "AN", "ANT", 530, 46);
			yield return new Country("New Caledonia", "NC", "NCL", 540, 977);
			yield return new Country("New Zealand", "NZ", "NZL", 554, 227);
			yield return new Country("Nicaragua", "NI", "NIC", 558, 691);
			yield return new Country("Niger", "NE", "NER", 562, 264);
			yield return new Country("Nigeria", "NG", "NGA", 566, 674);
			yield return new Country("Niue", "NU", "NIU", 570, 222);
			yield return new Country("Norfolk Island", "NF", "NFK", 574, 258);
			yield return new Country("Northern Mariana Islands", "MP", "MNP", 580, 599);
			yield return new Country("Norway", "NO", "NOR", 578, 52);
			yield return new Country("Oman", "OM", "OMN", 512, 234);
			yield return new Country("Pakistan", "PK", "PAK", 586, 92);
			yield return new Country("Palau", "PW", "PLW", 585, 675);
			yield return new Country("Palestinian Territory, Occupied", "PS", "PSE", null, 681);
			yield return new Country("Panama", "PA", "PAN", 591, 968);
			yield return new Country("Papua New Guinea", "PG", "PNG", 598, 505);
			yield return new Country("Paraguay", "PY", "PRY", 600, 595);
			yield return new Country("Peru", "PE", "PER", 604, 1670);
			yield return new Country("Philippines", "PH", "PHL", 608, 683);
			yield return new Country("Pitcairn", "PN", "PCN", 612, 680);
			yield return new Country("Poland", "PL", "POL", 616, 507);
			yield return new Country("Portugal", "PT", "PRT", 620, 500);
			yield return new Country("Puerto Rico", "PR", "PRI", 630, 672);
			yield return new Country("Qatar", "QA", "QAT", 634, 51);
			yield return new Country("Reunion", "RE", "REU", 638, 63);
			yield return new Country("Romania", "RO", "ROM", 642, 872);
			yield return new Country("Russian Federation", "RU", "RUS", 643, 290);
			yield return new Country("Rwanda", "RW", "RWA", 646, 212);
			yield return new Country("Saint Helena", "SH", "SHN", 654, 508);
			yield return new Country("Saint Kitts and Nevis", "KN", "KNA", 659, 351);
			yield return new Country("Saint Lucia", "LC", "LCA", 662, 996);
			yield return new Country("Saint Pierre and Miquelon", "PM", "SPM", 666, 39);
			yield return new Country("Saint Vincent and the Grenadines", "VC", "VCT", 670, 591);
			yield return new Country("Samoa", "WS", "WSM", 882, 221);
			yield return new Country("San Marino", "SM", "SMR", 674, 250);
			yield return new Country("Sao Tome and Principe", "ST", "STP", 678, 252);
			yield return new Country("Saudi Arabia", "SA", "SAU", 682, 65);
			yield return new Country("Senegal", "SN", "SEN", 686, 421);
			yield return new Country("Serbia and Montenegro", "CS", "SCG", null, 257);
			yield return new Country("Seychelles", "SC", "SYC", 690, 1784);
			yield return new Country("Sierra Leone", "SL", "SLE", 694, 262);
			yield return new Country("Singapore", "SG", "SGP", 702, 975);
			yield return new Country("Slovakia", "SK", "SVK", 703, 966);
			yield return new Country("Slovenia", "SI", "SVN", 705, 45);
			yield return new Country("Solomon Islands", "SB", "SLB", 90, 1869);
			yield return new Country("Somalia", "SO", "SOM", 706, 685);
			yield return new Country("South Africa", "ZA", "ZAF", 710, 967);
			yield return new Country("South Georgia and the South Sandwich Islands", "GS", "GSI", null, 995);
			yield return new Country("Spain", "ES", "ESP", 724, 248);
			yield return new Country("Sri Lanka", "LK", "LKA", 144, 686);
			yield return new Country("Sudan", "SD", "SDN", 736, 48);
			yield return new Country("Suriname", "SR", "SUR", 740, 239);
			yield return new Country("Svalbard and Jan Mayen", "SJ", "SJM", 744, 7);
			yield return new Country("Swaziland", "SZ", "SWZ", 748, null);
			yield return new Country("Sweden", "SE", "SWE", 752, 386);
			yield return new Country("Switzerland", "CH", "CHE", 756, 236);
			yield return new Country("Syrian Arab Republic", "SY", "SYR", 760, 1758);
			yield return new Country("Taiwan, Province of China", "TW", "TWN", 158, 1868);
			yield return new Country("Tajikistan", "TJ", "TJK", 762, 34);
			yield return new Country("Tanzania, United Republic of", "TZ", "TZA", 834, 66);
			yield return new Country("Thailand", "TH", "THA", 764, 94);
			yield return new Country("Timor-Leste", "TL", "TLS", 670, 886);
			yield return new Country("Togo", "TG", "TGO", 768, 249);
			yield return new Country("Tokelau", "TK", "TKL", 772, 597);
			yield return new Country("Tonga", "TO", "TON", 776, 41);
			yield return new Country("Trinidad and Tobago", "TT", "TTO", 780, 255);
			yield return new Country("Tunisia", "TN", "TUN", 788, 268);
			yield return new Country("Turkey", "TR", "TUR", 792, 963);
			yield return new Country("Turkmenistan", "TM", "TKM", 795, null);
			yield return new Country("Turks and Caicos Islands", "TC", "TCA", 796, 992);
			yield return new Country("Tuvalu", "TV", "TUV", 798, 235);
			yield return new Country("Uganda", "UG", "UGA", 800, 1649);
			yield return new Country("Ukraine", "UA", "UKR", 804, 670);
			yield return new Country("United Arab Emirates", "AE", "ARE", 784, 93);
			yield return new Country("United Kingdom", "GB", "GBR", 826, 679);
			yield return new Country("United States Minor Outlying Islands", "UM", "USM", 1, 216);
			yield return new Country("United States of America", "US", "USA", 840, 688);
			yield return new Country("Uruguay", "UY", "URY", 858, 1671);
			yield return new Country("Uzbekistan", "UZ", "UZB", 860, 40);
			yield return new Country("Vanuatu", "VU", "VUT", 548, 971);
			yield return new Country("Venezuela", "VE", "VEN", 862, 90);
			yield return new Country("Viet Nam", "VN", "VNM", 704, 380);
			yield return new Country("Virgin Islands, British", "VG", "VGB", 92, 378);
			yield return new Country("Virgin Islands, U.s.", "VI", "VIR", 850, 1664);
			yield return new Country("Wallis and Futuna", "WF", "WLF", 876, 44);
			yield return new Country("Western Sahara", "EH", "ESH", 732, 1809);
			yield return new Country("Yemen", "YE", "YEM", 887, 1);
			yield return new Country("Zambia", "ZM", "ZMB", 894, 260);
			yield return new Country("Zimbabwe", "ZW", "ZWE", 716, 263);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}