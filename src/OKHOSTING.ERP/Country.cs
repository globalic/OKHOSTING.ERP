using System;
using OKHOSTING.Data.Validation;

namespace OKHOSTING.ERP
{
	/// <summary>
	/// A country
	/// </summary>
	public class Country : ORM.PersistentClass<Guid>
	{
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
		public Int32? NumericCode
		{
			get;
			set;
		}

		/// <summary>
		/// Phone area code of the country
		/// </summary>
		/// <example>52 for México, 01 for USA</example>
		public Int32? PhoneCode
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

		/// <summary>
		/// Used for populating initial data only
		/// </summary>
		private Country Init(string name, string iso2, string iso3, int? numericCode, int? phoneCode)
		{
			Name = name;
			Iso2 = iso2;
			Iso3 = iso3;
			NumericCode = numericCode;
			PhoneCode = phoneCode;
			
			return this;
		}

		/// <summary>
		/// Returns the initial collection of countries that should be created on system setup
		/// </summary>
		/*
		public static List<XPBaseObject> GetSetupObjects(IObjectSpace db)
		{
			//if users already exist in database, return null
			if (db.GetObjects<Country>().Count > 0) return null;
			
			List<XPBaseObject> data = new List<XPBaseObject>();

			data.Add(db.CreateObject<Country>().Init("Afghanistan", "AF", "AFG", 4, 244));
			data.Add(db.CreateObject<Country>().Init("Albania", "AL", "ALB", 8, 376));
			data.Add(db.CreateObject<Country>().Init("Algeria", "DZ", "DZA", 12, 357));
			data.Add(db.CreateObject<Country>().Init("American Samoa", "AS", "ASM", 16, 1340));
			data.Add(db.CreateObject<Country>().Init("Andorra", "AD", "AND", 20, 377));
			data.Add(db.CreateObject<Country>().Init("Angola", "AO", "AGO", 24, 1268));
			data.Add(db.CreateObject<Country>().Init("Anguilla", "AI", "AIA", 660, 355));
			data.Add(db.CreateObject<Country>().Init("Antarctica", "AQ", "ATA", null, 994));
			data.Add(db.CreateObject<Country>().Init("Antigua and Barbuda", "AG", "ATG", 28, 1787));
			data.Add(db.CreateObject<Country>().Init("Argentina", "AR", "ARG", 32, 1684));
			data.Add(db.CreateObject<Country>().Init("Armenia", "AM", "ARM", 51, 373));
			data.Add(db.CreateObject<Country>().Init("Aruba", "AW", "ABW", 533, 374));
			data.Add(db.CreateObject<Country>().Init("Australia", "AU", "AUS", 36, 54));
			data.Add(db.CreateObject<Country>().Init("Austria", "AT", "AUT", 40, 1264));
			data.Add(db.CreateObject<Country>().Init("Azerbaijan", "AZ", "AZE", 31, 672));
			data.Add(db.CreateObject<Country>().Init("Bahamas", "BS", "BHS", 44, 229));
			data.Add(db.CreateObject<Country>().Init("Bahrain", "BH", "BHR", 48, 61));
			data.Add(db.CreateObject<Country>().Init("Bangladesh", "BD", "BGD", 50, 225));
			data.Add(db.CreateObject<Country>().Init("Barbados", "BB", "BRB", 52, 973));
			data.Add(db.CreateObject<Country>().Init("Belarus", "BY", "BLR", 112, 598));
			data.Add(db.CreateObject<Country>().Init("Belgium", "BE", "BEL", 56, 993));
			data.Add(db.CreateObject<Country>().Init("Belize", "BZ", "BLZ", 84, 501));
			data.Add(db.CreateObject<Country>().Init("Benin", "BJ", "BEN", 204, 32));
			data.Add(db.CreateObject<Country>().Init("Bermuda", "BM", "BMU", 60, 1284));
			data.Add(db.CreateObject<Country>().Init("Bhutan", "BT", "BTN", 64, 1242));
			data.Add(db.CreateObject<Country>().Init("Bolivia", "BO", "BOL", 68, 880));
			data.Add(db.CreateObject<Country>().Init("Bosnia and Herzegovina", "BA", "BIH", 70, 55));
			data.Add(db.CreateObject<Country>().Init("Botswana", "BW", "BWA", 72, 1246));
			data.Add(db.CreateObject<Country>().Init("Bouvet Island", "BV", "BVT", null, 387));
			data.Add(db.CreateObject<Country>().Init("Brazil", "BR", "BRA", 76, 375));
			data.Add(db.CreateObject<Country>().Init("British Indian Ocean Territory", "IO", "IOT", null, 39));
			data.Add(db.CreateObject<Country>().Init("Brunei Darussalam", "BN", "BRN", 96, 1441));
			data.Add(db.CreateObject<Country>().Init("Bulgaria", "BG", "BGR", 100, 267));
			data.Add(db.CreateObject<Country>().Init("Burkina Faso", "BF", "BFA", 854, 43));
			data.Add(db.CreateObject<Country>().Init("Burundi", "BI", "BDI", 108, 297));
			data.Add(db.CreateObject<Country>().Init("Cambodia", "KH", "KHM", 116, 962));
			data.Add(db.CreateObject<Country>().Init("Cameroon", "CM", "CMR", 120, 53));
			data.Add(db.CreateObject<Country>().Init("Canada", "CA", "CAN", 124, 237));
			data.Add(db.CreateObject<Country>().Init("Cape Verde", "CV", "CPV", 132, 243));
			data.Add(db.CreateObject<Country>().Init("Cayman Islands", "KY", "CYM", 136, 254));
			data.Add(db.CreateObject<Country>().Init("Central African Republic", "CF", "CAF", 140, 27));
			data.Add(db.CreateObject<Country>().Init("Chad", "TD", "TCD", 148, 238));
			data.Add(db.CreateObject<Country>().Init("Chile", "CL", "CHL", 152, 61));
			data.Add(db.CreateObject<Country>().Init("China", "CN", "CHN", 156, 57));
			data.Add(db.CreateObject<Country>().Init("Christmas Island", "CX", "CXR", 61, 678));
			data.Add(db.CreateObject<Country>().Init("Cocos (Keeling) Islands", "CC", "CCK", 61891, 226));
			data.Add(db.CreateObject<Country>().Init("Colombia", "CO", "COL", 170, 56));
			data.Add(db.CreateObject<Country>().Init("Comoros", "KM", "COM", 174, 241));
			data.Add(db.CreateObject<Country>().Init("Congo", "CG", "COG", 178, 1345));
			data.Add(db.CreateObject<Country>().Init("Congo, the Democratic Republic of the", "CD", "COD", 180, 58));
			data.Add(db.CreateObject<Country>().Init("Cook Islands", "CK", "COK", 184, 359));
			data.Add(db.CreateObject<Country>().Init("Costa Rica", "CR", "CRI", 188, 86));
			data.Add(db.CreateObject<Country>().Init("Cote D'Ivoire", "CI", "CIV", 384, 91));
			data.Add(db.CreateObject<Country>().Init("Croatia", "HR", "HRV", 191, 242));
			data.Add(db.CreateObject<Country>().Init("Cuba", "CU", "CUB", 192, 269));
			data.Add(db.CreateObject<Country>().Init("Cyprus", "CY", "CYP", 196, 855));
			data.Add(db.CreateObject<Country>().Init("Czech Republic", "CZ", "CZE", 203, 682));
			data.Add(db.CreateObject<Country>().Init("Denmark", "DK", "DNK", 208, 298));
			data.Add(db.CreateObject<Country>().Init("Djibouti", "DJ", "DJI", 262, 594));
			data.Add(db.CreateObject<Country>().Init("Dominica", "DM", "DMA", 212, 53));
			data.Add(db.CreateObject<Country>().Init("Dominican Republic", "DO", "DOM", 214, 385));
			data.Add(db.CreateObject<Country>().Init("Ecuador", "EC", "ECU", 218, 213));
			data.Add(db.CreateObject<Country>().Init("Egypt", "EG", "EGY", 818, 690));
			data.Add(db.CreateObject<Country>().Init("El Salvador", "SV", "SLV", 222, 677));
			data.Add(db.CreateObject<Country>().Init("Equatorial Guinea", "GQ", "GNQ", 226, 220));
			data.Add(db.CreateObject<Country>().Init("Eritrea", "ER", "ERI", 232, 232));
			data.Add(db.CreateObject<Country>().Init("Estonia", "EE", "EST", 233, 420));
			data.Add(db.CreateObject<Country>().Init("Ethiopia", "ET", "ETH", 231, 593));
			data.Add(db.CreateObject<Country>().Init("Falkland Islands (Malvinas)", "FK", "FLK", 238, 240));
			data.Add(db.CreateObject<Country>().Init("Faroe Islands", "FO", "FRO", 234, 372));
			data.Add(db.CreateObject<Country>().Init("Fiji", "FJ", "FJI", 242, 291));
			data.Add(db.CreateObject<Country>().Init("Finland", "FI", "FIN", 246, 1767));
			data.Add(db.CreateObject<Country>().Init("France", "FR", "FRA", 250, 20));
			data.Add(db.CreateObject<Country>().Init("French Guiana", "GF", "GUF", 254, 689));
			data.Add(db.CreateObject<Country>().Init("French Polynesia", "PF", "PYF", 258, 47));
			data.Add(db.CreateObject<Country>().Init("French Southern Territories", "TF", "TFS", null, 84));
			data.Add(db.CreateObject<Country>().Init("Gabon", "GA", "GAB", 266, 687));
			data.Add(db.CreateObject<Country>().Init("Gambia", "GM", "GMB", 270, 33));
			data.Add(db.CreateObject<Country>().Init("Georgia", "GE", "GEO", 268, 358));
			data.Add(db.CreateObject<Country>().Init("Germany", "DE", "DEU", 276, 506));
			data.Add(db.CreateObject<Country>().Init("Ghana", "GH", "GHA", 288, null));
			data.Add(db.CreateObject<Country>().Init("Gibraltar", "GI", "GIB", 292, 299));
			data.Add(db.CreateObject<Country>().Init("Greece", "GR", "GRC", 300, 49));
			data.Add(db.CreateObject<Country>().Init("Greenland", "GL", "GRL", 304, 233));
			data.Add(db.CreateObject<Country>().Init("Grenada", "GD", "GRD", 308, 676));
			data.Add(db.CreateObject<Country>().Init("Guadeloupe", "GP", "GLP", 312, 253));
			data.Add(db.CreateObject<Country>().Init("Guam", "GU", "GUM", 316, 251));
			data.Add(db.CreateObject<Country>().Init("Guatemala", "GT", "GTM", 320, 30));
			data.Add(db.CreateObject<Country>().Init("Guinea", "GN", "GIN", 324, 970));
			data.Add(db.CreateObject<Country>().Init("Guinea-Bissau", "GW", "GNB", 624, 350));
			data.Add(db.CreateObject<Country>().Init("Guyana", "GY", "GUY", 328, 1473));
			data.Add(db.CreateObject<Country>().Init("Haiti", "HT", "HTI", 332, 590));
			data.Add(db.CreateObject<Country>().Init("Heard Island and Mcdonald Islands", "HM", "HMI", null, 1284));
			data.Add(db.CreateObject<Country>().Init("Holy See (Vatican City State)", "VA", "VAT", 336, 256));
			data.Add(db.CreateObject<Country>().Init("Honduras", "HN", "HND", 340, 502));
			data.Add(db.CreateObject<Country>().Init("Hong Kong", "HK", "HKG", 344, 224));
			data.Add(db.CreateObject<Country>().Init("Hungary", "HU", "HUN", 348, 245));
			data.Add(db.CreateObject<Country>().Init("Iceland", "IS", "ISL", 352, 36));
			data.Add(db.CreateObject<Country>().Init("India", "IN", "IND", 356, 852));
			data.Add(db.CreateObject<Country>().Init("Indonesia", "ID", "IDN", 360, null));
			data.Add(db.CreateObject<Country>().Init("Iran, Islamic Republic of", "IR", "IRN", 364, 592));
			data.Add(db.CreateObject<Country>().Init("Iraq", "IQ", "IRQ", 368, 504));
			data.Add(db.CreateObject<Country>().Init("Ireland", "IE", "IRL", 372, 509));
			data.Add(db.CreateObject<Country>().Init("Israel", "IL", "ISR", 376, 998));
			data.Add(db.CreateObject<Country>().Init("Italy", "IT", "ITA", 380, 354));
			data.Add(db.CreateObject<Country>().Init("Jamaica", "JM", "JAM", 388, 62));
			data.Add(db.CreateObject<Country>().Init("Japan", "JP", "JPN", 392, 98));
			data.Add(db.CreateObject<Country>().Init("Jordan", "JO", "JOR", 400, 353));
			data.Add(db.CreateObject<Country>().Init("Kazakhstan", "KZ", "KAZ", 398, 82));
			data.Add(db.CreateObject<Country>().Init("Kenya", "KE", "KEN", 404, 964));
			data.Add(db.CreateObject<Country>().Init("Kiribati", "KI", "KIR", 296, 673));
			data.Add(db.CreateObject<Country>().Init("Korea, Democratic People's Republic of", "KP", "PRK", 408, 1876));
			data.Add(db.CreateObject<Country>().Init("Korea, Republic of", "KR", "KOR", 410, 81));
			data.Add(db.CreateObject<Country>().Init("Kuwait", "KW", "KWT", 414, 7));
			data.Add(db.CreateObject<Country>().Init("Kyrgyzstan", "KG", "KGZ", 417, 972));
			data.Add(db.CreateObject<Country>().Init("Lao People's Democratic Republic", "LA", "LAO", 418, 965));
			data.Add(db.CreateObject<Country>().Init("Latvia", "LV", "LVA", 428, 692));
			data.Add(db.CreateObject<Country>().Init("Lebanon", "LB", "LBN", 422, 974));
			data.Add(db.CreateObject<Country>().Init("Lesotho", "LS", "LSO", 426, 371));
			data.Add(db.CreateObject<Country>().Init("Liberia", "LR", "LBR", 430, 856));
			data.Add(db.CreateObject<Country>().Init("Libyan Arab Jamahiriya", "LY", "LBY", 434, 60));
			data.Add(db.CreateObject<Country>().Init("Liechtenstein", "LI", "LIE", 438, 1));
			data.Add(db.CreateObject<Country>().Init("Lithuania", "LT", "LTU", 440, 850));
			data.Add(db.CreateObject<Country>().Init("Luxembourg", "LU", "LUX", 442, 961));
			data.Add(db.CreateObject<Country>().Init("Macao", "MO", "MAC", 446, 960));
			data.Add(db.CreateObject<Country>().Init("Macedonia, the Former Yugoslav Republic of", "MK", "MKD", 807, 228));
			data.Add(db.CreateObject<Country>().Init("Madagascar", "MG", "MDG", 450, 265));
			data.Add(db.CreateObject<Country>().Init("Malawi", "MW", "MWI", 454, 218));
			data.Add(db.CreateObject<Country>().Init("Malaysia", "MY", "MYS", 458, 230));
			data.Add(db.CreateObject<Country>().Init("Maldives", "MV", "MDV", 462, 231));
			data.Add(db.CreateObject<Country>().Init("Mali", "ML", "MLI", 466, 370));
			data.Add(db.CreateObject<Country>().Init("Malta", "MT", "MLT", 470, 853));
			data.Add(db.CreateObject<Country>().Init("Marshall Islands", "MH", "MHL", 584, 64));
			data.Add(db.CreateObject<Country>().Init("Martinique", "MQ", "MTQ", 474, 596));
			data.Add(db.CreateObject<Country>().Init("Mauritania", "MR", "MRT", 478, 389));
			data.Add(db.CreateObject<Country>().Init("Mauritius", "MU", "MUS", 480, 261));
			data.Add(db.CreateObject<Country>().Init("Mayotte", "YT", "MYT", null, 381));
			data.Add(db.CreateObject<Country>().Init("Mexico", "MX", "MEX", 484, 1340));
			data.Add(db.CreateObject<Country>().Init("Micronesia, Federated States of", "FM", "FSM", 583, 503));
			data.Add(db.CreateObject<Country>().Init("Moldova, Republic of", "MD", "MDA", 498, 423));
			data.Add(db.CreateObject<Country>().Init("Monaco", "MC", "MCO", 492, 266));
			data.Add(db.CreateObject<Country>().Init("Mongolia", "MN", "MNG", 496, null));
			data.Add(db.CreateObject<Country>().Init("Montserrat", "MS", "MSR", 500, 356));
			data.Add(db.CreateObject<Country>().Init("Morocco", "MA", "MAR", 504, 223));
			data.Add(db.CreateObject<Country>().Init("Mozambique", "MZ", "MOZ", 508, 976));
			data.Add(db.CreateObject<Country>().Init("Myanmar", "MM", "MMR", 104, 352));
			data.Add(db.CreateObject<Country>().Init("Namibia", "NA", "NAM", 516, 95));
			data.Add(db.CreateObject<Country>().Init("Nauru", "NR", "NRU", 520, 212));
			data.Add(db.CreateObject<Country>().Init("Nepal", "NP", "NPL", 524, 269));
			data.Add(db.CreateObject<Country>().Init("Netherlands", "NL", "NLD", 528, 31));
			data.Add(db.CreateObject<Country>().Init("Netherlands Antilles", "AN", "ANT", 530, 46));
			data.Add(db.CreateObject<Country>().Init("New Caledonia", "NC", "NCL", 540, 977));
			data.Add(db.CreateObject<Country>().Init("New Zealand", "NZ", "NZL", 554, 227));
			data.Add(db.CreateObject<Country>().Init("Nicaragua", "NI", "NIC", 558, 691));
			data.Add(db.CreateObject<Country>().Init("Niger", "NE", "NER", 562, 264));
			data.Add(db.CreateObject<Country>().Init("Nigeria", "NG", "NGA", 566, 674));
			data.Add(db.CreateObject<Country>().Init("Niue", "NU", "NIU", 570, 222));
			data.Add(db.CreateObject<Country>().Init("Norfolk Island", "NF", "NFK", 574, 258));
			data.Add(db.CreateObject<Country>().Init("Northern Mariana Islands", "MP", "MNP", 580, 599));
			data.Add(db.CreateObject<Country>().Init("Norway", "NO", "NOR", 578, 52));
			data.Add(db.CreateObject<Country>().Init("Oman", "OM", "OMN", 512, 234));
			data.Add(db.CreateObject<Country>().Init("Pakistan", "PK", "PAK", 586, 92));
			data.Add(db.CreateObject<Country>().Init("Palau", "PW", "PLW", 585, 675));
			data.Add(db.CreateObject<Country>().Init("Palestinian Territory, Occupied", "PS", "PSE", null, 681));
			data.Add(db.CreateObject<Country>().Init("Panama", "PA", "PAN", 591, 968));
			data.Add(db.CreateObject<Country>().Init("Papua New Guinea", "PG", "PNG", 598, 505));
			data.Add(db.CreateObject<Country>().Init("Paraguay", "PY", "PRY", 600, 595));
			data.Add(db.CreateObject<Country>().Init("Peru", "PE", "PER", 604, 1670));
			data.Add(db.CreateObject<Country>().Init("Philippines", "PH", "PHL", 608, 683));
			data.Add(db.CreateObject<Country>().Init("Pitcairn", "PN", "PCN", 612, 680));
			data.Add(db.CreateObject<Country>().Init("Poland", "PL", "POL", 616, 507));
			data.Add(db.CreateObject<Country>().Init("Portugal", "PT", "PRT", 620, 500));
			data.Add(db.CreateObject<Country>().Init("Puerto Rico", "PR", "PRI", 630, 672));
			data.Add(db.CreateObject<Country>().Init("Qatar", "QA", "QAT", 634, 51));
			data.Add(db.CreateObject<Country>().Init("Reunion", "RE", "REU", 638, 63));
			data.Add(db.CreateObject<Country>().Init("Romania", "RO", "ROM", 642, 872));
			data.Add(db.CreateObject<Country>().Init("Russian Federation", "RU", "RUS", 643, 290));
			data.Add(db.CreateObject<Country>().Init("Rwanda", "RW", "RWA", 646, 212));
			data.Add(db.CreateObject<Country>().Init("Saint Helena", "SH", "SHN", 654, 508));
			data.Add(db.CreateObject<Country>().Init("Saint Kitts and Nevis", "KN", "KNA", 659, 351));
			data.Add(db.CreateObject<Country>().Init("Saint Lucia", "LC", "LCA", 662, 996));
			data.Add(db.CreateObject<Country>().Init("Saint Pierre and Miquelon", "PM", "SPM", 666, 39));
			data.Add(db.CreateObject<Country>().Init("Saint Vincent and the Grenadines", "VC", "VCT", 670, 591));
			data.Add(db.CreateObject<Country>().Init("Samoa", "WS", "WSM", 882, 221));
			data.Add(db.CreateObject<Country>().Init("San Marino", "SM", "SMR", 674, 250));
			data.Add(db.CreateObject<Country>().Init("Sao Tome and Principe", "ST", "STP", 678, 252));
			data.Add(db.CreateObject<Country>().Init("Saudi Arabia", "SA", "SAU", 682, 65));
			data.Add(db.CreateObject<Country>().Init("Senegal", "SN", "SEN", 686, 421));
			data.Add(db.CreateObject<Country>().Init("Serbia and Montenegro", "CS", "SCG", null, 257));
			data.Add(db.CreateObject<Country>().Init("Seychelles", "SC", "SYC", 690, 1784));
			data.Add(db.CreateObject<Country>().Init("Sierra Leone", "SL", "SLE", 694, 262));
			data.Add(db.CreateObject<Country>().Init("Singapore", "SG", "SGP", 702, 975));
			data.Add(db.CreateObject<Country>().Init("Slovakia", "SK", "SVK", 703, 966));
			data.Add(db.CreateObject<Country>().Init("Slovenia", "SI", "SVN", 705, 45));
			data.Add(db.CreateObject<Country>().Init("Solomon Islands", "SB", "SLB", 90, 1869));
			data.Add(db.CreateObject<Country>().Init("Somalia", "SO", "SOM", 706, 685));
			data.Add(db.CreateObject<Country>().Init("South Africa", "ZA", "ZAF", 710, 967));
			data.Add(db.CreateObject<Country>().Init("South Georgia and the South Sandwich Islands", "GS", "GSI", null, 995));
			data.Add(db.CreateObject<Country>().Init("Spain", "ES", "ESP", 724, 248));
			data.Add(db.CreateObject<Country>().Init("Sri Lanka", "LK", "LKA", 144, 686));
			data.Add(db.CreateObject<Country>().Init("Sudan", "SD", "SDN", 736, 48));
			data.Add(db.CreateObject<Country>().Init("Suriname", "SR", "SUR", 740, 239));
			data.Add(db.CreateObject<Country>().Init("Svalbard and Jan Mayen", "SJ", "SJM", 744, 7));
			data.Add(db.CreateObject<Country>().Init("Swaziland", "SZ", "SWZ", 748, null));
			data.Add(db.CreateObject<Country>().Init("Sweden", "SE", "SWE", 752, 386));
			data.Add(db.CreateObject<Country>().Init("Switzerland", "CH", "CHE", 756, 236));
			data.Add(db.CreateObject<Country>().Init("Syrian Arab Republic", "SY", "SYR", 760, 1758));
			data.Add(db.CreateObject<Country>().Init("Taiwan, Province of China", "TW", "TWN", 158, 1868));
			data.Add(db.CreateObject<Country>().Init("Tajikistan", "TJ", "TJK", 762, 34));
			data.Add(db.CreateObject<Country>().Init("Tanzania, United Republic of", "TZ", "TZA", 834, 66));
			data.Add(db.CreateObject<Country>().Init("Thailand", "TH", "THA", 764, 94));
			data.Add(db.CreateObject<Country>().Init("Timor-Leste", "TL", "TLS", 670, 886));
			data.Add(db.CreateObject<Country>().Init("Togo", "TG", "TGO", 768, 249));
			data.Add(db.CreateObject<Country>().Init("Tokelau", "TK", "TKL", 772, 597));
			data.Add(db.CreateObject<Country>().Init("Tonga", "TO", "TON", 776, 41));
			data.Add(db.CreateObject<Country>().Init("Trinidad and Tobago", "TT", "TTO", 780, 255));
			data.Add(db.CreateObject<Country>().Init("Tunisia", "TN", "TUN", 788, 268));
			data.Add(db.CreateObject<Country>().Init("Turkey", "TR", "TUR", 792, 963));
			data.Add(db.CreateObject<Country>().Init("Turkmenistan", "TM", "TKM", 795, null));
			data.Add(db.CreateObject<Country>().Init("Turks and Caicos Islands", "TC", "TCA", 796, 992));
			data.Add(db.CreateObject<Country>().Init("Tuvalu", "TV", "TUV", 798, 235));
			data.Add(db.CreateObject<Country>().Init("Uganda", "UG", "UGA", 800, 1649));
			data.Add(db.CreateObject<Country>().Init("Ukraine", "UA", "UKR", 804, 670));
			data.Add(db.CreateObject<Country>().Init("United Arab Emirates", "AE", "ARE", 784, 93));
			data.Add(db.CreateObject<Country>().Init("United Kingdom", "GB", "GBR", 826, 679));
			data.Add(db.CreateObject<Country>().Init("United States Minor Outlying Islands", "UM", "USM", 1, 216));
			data.Add(db.CreateObject<Country>().Init("United States of America", "US", "USA", 840, 688));
			data.Add(db.CreateObject<Country>().Init("Uruguay", "UY", "URY", 858, 1671));
			data.Add(db.CreateObject<Country>().Init("Uzbekistan", "UZ", "UZB", 860, 40));
			data.Add(db.CreateObject<Country>().Init("Vanuatu", "VU", "VUT", 548, 971));
			data.Add(db.CreateObject<Country>().Init("Venezuela", "VE", "VEN", 862, 90));
			data.Add(db.CreateObject<Country>().Init("Viet Nam", "VN", "VNM", 704, 380));
			data.Add(db.CreateObject<Country>().Init("Virgin Islands, British", "VG", "VGB", 92, 378));
			data.Add(db.CreateObject<Country>().Init("Virgin Islands, U.s.", "VI", "VIR", 850, 1664));
			data.Add(db.CreateObject<Country>().Init("Wallis and Futuna", "WF", "WLF", 876, 44));
			data.Add(db.CreateObject<Country>().Init("Western Sahara", "EH", "ESH", 732, 1809));
			data.Add(db.CreateObject<Country>().Init("Yemen", "YE", "YEM", 887, 1));
			data.Add(db.CreateObject<Country>().Init("Zambia", "ZM", "ZMB", 894, 260));
			data.Add(db.CreateObject<Country>().Init("Zimbabwe", "ZW", "ZWE", 716, 263));

			return data;
		}
		*/
	}
}