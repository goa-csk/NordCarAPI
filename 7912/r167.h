#ifdef egekeydef

key1 = kunde_nr
key2 = navn_2(10) navn_1(10) kunde_nr
key3 = postkode(10) kunde_nr
key4 = tlf kunde_nr
key5 = foedselsdato checkcif kunde_nr
key6 = kkortnr kunde_nr
key7 = pasnr kunde_nr
key8 = debitor_reference kunde_nr
key9 = firma_reference kunde_nr
key10 = recap_nr kunde_nr
key11 = gw_id kunde_nr
key12 = status kunde_nr
key13 = iata_id kunde_nr
key14 = dublet_henv kunde_nr

#endif

typedef struct {
long int	kunde_nr;			/*:1121*/
short int	type;				/*:1122*/
char		navn_1 [21];			/*:1123*/
char		navn_2 [31];			/*:1123*/
char		adr_1 [31];			/*:1124*/
char		adr_2 [31];			/*:1124*/
char		postkode [16];			/*:1124*/
char		by [16];			/*:1124*/
char		land [11];			/*:1124*/
char		tlf [21];			/*:1124*/
da_aammdd	foedselsdato;			/*:1125*/
short int	checkcif;			/*:1126*/
char		kkortnr [16];			/*:1127*/
char		kkort_ud_af [16] ;		/*:1127*/
da_aammdd	kkort_ud_den;			/*:1127*/
char		pasnr [16];			/*:1128*/
char		pas_ud_af [16];			/*:1128*/
da_aammdd	pas_ud_den;			/*:1128*/
da_aammdd	pas_gyldig_til;			/*:1128*/
char		rabat_bet [31];			/*:1129*/
double		rabat_lok;			/*:1129*/
double		rabat_serv;			/*:1129*/
double		rabat_int;			/*:1129*/
char		foede_sted[16];			/*:1127*/
char		recap_nr [11];			/*:1130*/
long int	firma_reference;		/*:1131*/
long int	debitor_reference;		/*:1131*/
da_aammdd	oprettelses_dato;		/*:1135*/
short int	status;				/*:1132*/
char		bem [31];			/*:1132*/
short int	dk_ja_nej;			/*:1133*/
short int	station_nr;			/*:1136*/
long int	stat_reference;			/*:1131*/
long int	ref_reference;			/*:1131*/
long int	kunde_ref;			/*:1131*/
short int	statistik_flag;			/*:1134*/
long int	gw_id;				/*:1131*/
long int	iata_id;			/*:1131*/
short int	forsikring;			/*:1137*/
short int	retprog;			/*:1137*/
da_aammdd 	ret_dato;			/*:1137*/
long int	dublet_henv;			/*:1138*/
filchar		filler [14];
} R167;
