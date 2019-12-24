	//////////////////////////////////////////Enk1
    eval(function(p,a,c,k,e,d){e=function(c){return c};if(!''.replace(/^/,String)){while(c--){d[c]=k[c]||c}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('0 12;0 13="#14";0 11="8/";0 10="3://4.9.5.2/4/15/";0 25="3://23.24.5.2/20/17/";0 19=1 22();6.18=1 7();6.16=1 7();21();',10,26,'var|new|gov|ftp|igscb|nasa|QueryString|Array|images|jpl|gIGSBaseURL|gImageDir|gYear|borderColor|006699|product|values|products|keys|now|glonass|QueryString_Parse|Date|cddis|gsfc|gGLOBaseURL'.split('|'),0,{}))

    //////////////////////////////////////////Kraj Enk1

    
    function QueryString(key)
    {
    	var value = null;
    	for (var i=0;i<QueryString.keys.length;i++)
    	{
    		if (QueryString.keys[i]==key)
    		{
    			value = QueryString.values[i];
    			break;
    		}
    	}
    	return value;
    }
    
    function QueryString_Parse()
    {
    	var query = window.location.search.substring(1);
    	var pairs = query.split("&");
    	
    	for (var i=0;i<pairs.length;i++)
    	{
    		var pos = pairs[i].indexOf('=');
    		if (pos >= 0)
    		{
    			var argname = pairs[i].substring(0,pos);
    			var value = pairs[i].substring(pos+1);
    			QueryString.keys[QueryString.keys.length] = argname;
    			QueryString.values[QueryString.values.length] = value;		
    		}
    	}
    }
    
	//////////////////////////////////////////Enk2
    eval(function(p,a,c,k,e,d){e=function(c){return c};if(!''.replace(/^/,String)){while(c--){d[c]=k[c]||c}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('19 37(3,24,6){7 11=6;7 14=3;17(3<=2){11--;14+=12}7 15=10.9(11/21);7 23=2-15+10.9(15/4);20 10.9(38.25*11)+10.9(30.39*(14+1))+24+44.5+23}19 36(3,22,6){17(6<32)6=6+33;7 18=13 16(31,28,31,30,31,30,31,31,30,31,30,31);17(((6%4==0)&&(6%21!=0))||(6%42==0))18[1]=29;8=22;3=3-1;63(3>-1){8=8+18[3];3=3-1}20 8}7 57=58.50;26.49=13 16("48","47","51","52","56","55","54");26.53=13 16("35","61","60","46","34","41","40","45","43","27","59","62");',10,64,'|||month|||year|var|doy|floor|Math|y||new|m|A|Array|if|eom|function|return|100|date|B|day||Calendar|October|||||2000|1900|May|January|DOY|getJulian|365|6001|July|June|400|September|1720994|August|April|M|Su|dayName|500|Tu|W|monthName|Sa|F|Th|gpsZero|2444244|November|March|February|December|while'.split('|'),0,{}))

	//////////////////////////////////////////Kraj Enk2
    				
    function Calendar_GetString() //ISPIS KALENDARA MJESECI I DANI
    {
    	//Create Date object at noon, seems to fix BST issue
    	//var firstDate = new Date(this.year,this.month,1,12,0,0);
    	//Create Date object at 14:00, which seems to correct NZT bug
    	var firstDate = new Date(this.year,this.month,1,14,0,0);
    	var firstDay = firstDate.getUTCDay();
    	
		
    	var calStr = "<TABLE BORDER=0 COLS=7 cellpadding=0>"; //Ovaj dio je onaj dio koji pokazuje kompletan kalendar ALI OVO JE ZA SVAKI MJESEC JER JEDAN MJESEC JE TABELA
    	calStr += "<TR>";
    	calStr += "<TD COLSPAN=7 ALIGN=center class=\"kalendar_naziv_mjeseca\">"+Calendar.monthName[this.month].toUpperCase()+" "+this.year+"</TD>"; //U Kalendaru naziv mjeseca
    	calStr += "</TR>";
    	calStr += "<TR>";
    
    	for (var i=0;i<Calendar.dayName.length;i++) 
    		calStr += "<TD ALIGN=center  class=\"kalendar_naziv_dana\">"+Calendar.dayName[i]+"</TD>"; //Prikazuju se skraceni nazivi dana MON TUE WED itd.
    	calStr += "</TR>";
    
    	var dayCount = 1;
    	calStr += "<TR>";
    	for (var i=0;i<firstDay;i++) 
    		calStr += "<TD class=\"kalendar_dan_u_mjesecu\"> </TD>"; //Prva polja koja ce biti prazna u mjesecu (dakle dani)
    
    	var monthArg = this.month + 1;
    	for (var i=0;i<this.monthDays[this.month];i++)
    	{
    		var styleStr = "kalendar_dan_u_mjesecu";
    		if (dayCount==this.date)
    			styleStr = "kalendar_dan_u_mjesecu_danasnji";
    			
    	    
    		var statusLine = " onmouseover=\"window.status='" + monthArg + "/" + dayCount +	"/" + this.year + "';return true;\" onmouseout=\"window.status='';\"";
    			
    		calStr += '<TD class=\"'+styleStr+'\"><A HREF="javascript:void(HandleCalClick'+
    					'('+dayCount+','+monthArg+','+this.year+'))"' + statusLine + '>'+
    					dayCount+'</A></FONT></TD>'; //Datum gdje kad kliknes prikaze ti detalje dana
    		dayCount++;
    		if ((i+firstDay+1)%7==0&&(dayCount<this.monthDays[this.month]+1)) 
    			calStr += "</TR><TR>";
    	}
    
    	var totCells = firstDay+this.monthDays[this.month];
    	for (var i=0;i<(totCells>28?(totCells>35?42:35):28)-totCells;i++) 
    		calStr += "<TD class=\"kalendar_dan_u_mjesecu\"> </TD>" //Celije u tabeli gdje treba da idu prazna mjesta u danima
    	calStr += "</TR>";
    	calStr += "</TABLE>";
    	return calStr;
    }
    
    function Calendar_Show()
    {
    	var calStr = this.GetString();
    	document.write(calStr);
    }
    
    function Calendar(date,month,year)
    {
    	this.date = date;
    	this.month = month;
    	this.year = year;
    	this.monthDays = new Array(31,28,31,30,31,30,31,31,30,31,30,31);
    	
    	if (((this.year % 4 == 0) && (this.year % 100 != 0)) || (this.year % 400 == 0)) 
    		this.monthDays[1] = 29; 
    	else 
    		this.monthDays[1] = 28;
    		
    	this.Show = Calendar_Show;
    	this.GetString = Calendar_GetString;
    }
    
	//////////////////////////////////////////Enk3
    eval(function(p,a,c,k,e,d){e=function(c){return c};if(!''.replace(/^/,String)){while(c--){d[c]=k[c]||c}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('13 31(){3 30=\'\';5.6(\'<33 19="51" 30="0" 52="1" 53="0" 54="50"><16>\');3 17=0;20(3 2=0;2<12;2++){17++;11(17>4){17=1;5.6(\'</16><16>\')}5.6(\'<28 45=46>\');14.27[2].34();5.6(\'</28>\')}5.6(\'</16></33>\');38();36()}13 55(){3 18=0;10=7.26();3 8=41("9");11(42!=8){10=25(8)}3 32=12;14.27=21 62();20(3 2=0;2<32;2++){3 22=0;11((7.26()==10)&&(7.40()==18))22=7.44();14.27[2]=21 60(22,18,10);18++}14.34=31}13 36(){3 29=21 58();3 35=29.61();5.6("<15 19=\\"56\\">\\39");20(3 2=64,37=0;2<=35;2++,37++){11(10==2){5.6(\'<15 19="47">\'+2+\'</15>\')}57{5.6(\'<43 19="59" 48="63.65?9=\'+2+\'">\'+2+\'</43>\')}}5.6("</15>\\39")}13 38(){3 23=7.40();3 24=7.44();3 9=7.26();3 8=41("9");11(42!=8&&25(8)!=9){9=25(8);23=0;24=1}49(24,23+1,9)}',10,66,'||i|var||document|write|now|queryYear|year|gYear|if||function|this|div|tr|counter|month|class|for|new|dayToHilite|mon|day|parseInt|getUTCFullYear|calendars|td|d|border|CalendarSet_Show|months|table|Show|godina_ik|showLinks|j|showCurrentData|n|getUTCMonth|QueryString|null|a|getUTCDate|valign|top|mjesec_aktivni|href|HandleCalClick|center|globalna_tabela_kalendar|cellpadding|cellspacing|align|CalendarSet|izbor_mjeseci_container|else|Date|mjesec|Calendar|getFullYear|Array|index|1994|html'.split('|'),0,{}))

	//////////////////////////////////////////Kraj Enk3

    function HandleCalClick(day,mon,year)
    {
        $(document).ready(function() {
            $('.tabela_gore_detalji a').each(function() {
                $(this).attr("href", function(index, old) {
                    return old.replace("ftp://igscb.jpl.nasa.gov/igscb/product/", "ftp://cddis.gsfc.nasa.gov/gnss/products/");
                });
                $(this).attr("href", function(index, old) {
                    return old.replace("http://igscb.jpl.nasa.gov/igscb/product/", "ftp://cddis.gsfc.nasa.gov/gnss/products/");
                });
                $(this).attr("href", function(index, old) {
                    return old.replace("ftp://ftp.unibe.ch/aiub/CODE/", "http://ftp.aiub.unibe.ch/CODE/");
                });
            });
        });

		//////////////////////////////////////////Enk4
        eval(function(p,a,c,k,e,d){e=function(c){return c};if(!''.replace(/^/,String)){while(c--){d[c]=k[c]||c}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('0 11=5(10,4,12);0 15=13.24((11-20)/7);0 21=6 22("23","18","16","17","14","19","31");0 2=6 34();0 33=5(2.25()+1,2.36(),2.37());0 35="";0 32="";0 27=3.9?3.9("8"):3.26.8;0 28=10-1;0 29=12;0 30=4;',10,38,'var||now|document|day|getJulian|new||caldata|getElementById|mon|jd|year|Math|Thursday|gpsw|Tuesday|Wednesday|Monday|Friday|gpsZero|dname|Array|Sunday|floor|getUTCMonth|all|thelement|UTCMonth|UTCFullYear|UTCDay|Saturday|ephLinks2|jdn|Date|ephLinks|getUTCDate|getUTCFullYear'.split('|'),0,{}))

		//////////////////////////////////////////Kraj Enk4
		
        var dow = (jd % 7) + 1.5;
        if(dow > 6) dow -= 7;
        
        
        if (!document.all && !document.getElementById){
            return;
        }
		
		//////////////////////////////////////////Enk5
        eval(function(p,a,c,k,e,d){e=function(c){return c};if(!''.replace(/^/,String)){while(c--){d[c]=k[c]||c}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('19((31-29)>18&&4>=67){70="<14><3 6=2 13=21>57 69 46 (52): </3><3 6=2 13=20>"+"<12 22="+63+(4<23?"0":"")+4+"/45"+(4<23?"0":"")+4+24+".38.11>45"+(4<23?"0":"")+4+24+".38.11</12></3></14>"}27 19((31-29)>1&&4>=67){70="<14><3 6=2 13=21>57 86 46 (52): </3><3 6=2 13=20>"+"<12 22="+63+(4<23?"0":"")+4+"/68"+(4<23?"0":"")+4+24+".38.11>68"+(4<23?"0":"")+4+24+".38.11</12></3></14>"}19((31-29)>18&&4>=85){87="<14><3 6=2 13=21>88 69 46 (84): </3><3 6=2 13=20>"+"<12 22="+89+(4<23?"0":"")+4+"/64"+(4<23?"0":"")+4+24+".38.11>64"+(4<23?"0":"")+4+24+".38.11</12></3></14>"}19((31-29)>=1){15 32=78(49-1,83,17);15 33;19(32<=9){33="82"+32}27 19(32>=10&&32<=99){33="0"+32}27{33=32}15 35=17.56();15 26=35.75(2,35.53);60="<14><3 6=2 13=21>66 57 92 46 (66): </3><3 6=2 13=20><12 22=\\"16://102.104.61.62/101/100/94/"+17+"/58/58"+33+"0."+26+"65.11\\">58"+33+"0."+26+"65.11</12></3></14>"}27{60=""}19((31-29)>=18){15 39=4;15 25=4+""+24;72="<14><3 6=2 13=21>98 97 44 30 96 (52): </3><3 6=2 13=20><12 22=\\"95://71.105.61.62/71/90/"+39+"/45"+25+".77.11\\">45"+25+".77.11</12></3></14>"}27{72=""}19((31-29)>=5){15 25=4+""+24;50="<14><3 6=2 13=21>73 54 44: </3><3 6=2 13=20><12 22=\\"16://16.42.41/40/43/"+17+"/34"+25+".54.11\\">34"+25+".54.11</12></3></14>";50+="<14><3 6=2 13=21>73 36 44: </3><3 6=2 13=20><12 22=\\"16://16.42.41/40/43/"+17+"/34"+25+".36.11\\">34"+25+".36.11</12></3></14>"}27{50=""}19((31-29)>=8){15 39=4;76="<14><3 6=2 13=21>36 44 93 103: </3><3 6=2 13=20><12 22=\\"16://16.42.41/40/43/"+17+"/34"+39+"7.36.11\\">34"+39+"7.36.11</12></3></14>"}27{76=""}15 48=80 79();15 74=48.81()+1;15 59=48.91();19(17<59||(59==17&&49<74-1)){15 35=17.56();15 26=35.75(2,35.53);15 28=49.56();19(28.53==1){28="0"+28}51="<14><3 6=2 13=21>55 37: </3><3 6=2 13=20><12 22=\\"16://16.42.41/40/43/"+17+"/55"+26+28+".37.11\\">55"+26+28+".37.11</12></3></14>";51+="<14><3 6=2 13=21>47 37: </3><3 6=2 13=20><12 22=\\"16://16.42.41/40/43/"+17+"/47"+26+28+".37.11\\">47"+26+28+".37.11</12></3></14>"}27{51=""}',10,106,'|||td|gpsw||colspan|||||Z|a|align|tr|var|ftp|year||if|left|right|href|1000|dow|gps_week_number|dva_zadnja_broja_godine|else|mjesec_string|jd||jdn|danugodini|format_tekst|COD|godina_string|SNX|DCB|sp3|gps_week|aiub|ch|unibe|CODE|file|igs|Orbits|P1P2|d|mon|aiublinkovi|aiubmjesecilinkovi|IGS|length|ION|P1C1|toString|GPS|brdc|godina_ik|brdclinkovi|nasa|gov|gIGSBaseURL|igl|n|BRDC|720|igr|Final|ephLinks|igscb|igscblinkovi|AIUB|mjesec_ik|substr|aiubsedmicelinkovi|clk_30s|DOY|Date|new|getMonth|00|day|CDDIS|1278|Rapid|ephLinks2|GlONASS|gGLOBaseURL|product|getFullYear|Broadcast|for|daily|http|s|Clock|IGSCB||data|gnss|cddis|Week|gsfc|jpl'.split('|'),0,{}))

		//////////////////////////////////////////Kraj Enk5
		
		//////////////////////////////////////////Enk6
		eval(function(p,a,c,k,e,d){e=function(c){return c};if(!''.replace(/^/,String)){while(c--){d[c]=k[c]||c}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('26.27="<10 5=\\"22\\" 16=\\"0\\" 29=\\"1\\" 30=\\"1\\"  28=31>\\3"+"  <6>\\3"+"    <2 24=\\"4\\" 5=\\"23\\">"+17[15]+", "+18.19[21]+" "+20+", "+25+" (42)</2>\\3"+"  </6>\\3"+"  <6>\\3"+"    <2 5=\\"8\\">46 9 14: </2>\\3"+"    <2 5=\\"7\\">"+45+"</2>\\3"+"    <2 5=\\"8\\">9 47 43: </2>\\3"+"    <2 5=\\"7\\">"+32(44-1,41,40)+"</2>\\3"+"  </6>\\3"+"  <6>\\3"+"    <2 5=\\"8\\">12 11: </2>\\3"+"    <2 5=\\"7\\">"+13+"</2>\\3"+"    <2 5=\\"8\\">12 11 14: </2>\\3"+"    <2 5=\\"7\\">"+13+15+"</2>\\3"+"  </6>\\3"+35+34+33+36+37+39+38+"</10>\\3";',10,48,'||td|n||class|tr|vrijednost|naslov|Day|table|Week|GPS|gpsw|Number|dow|border|dname|Calendar|monthName|UTCDay|UTCMonth|tabela_gore_detalji|datum_nedelja_dan|colspan|UTCFullYear|thelement|innerHTML|align|cellpadding|cellspacing|center|DOY|brdclinkovi|ephLinks2|ephLinks|igscblinkovi|aiublinkovi|aiubmjesecilinkovi|aiubsedmicelinkovi|year|day|UTC|Year|mon|jd|Julian|of'.split('|'),0,{}))

		//////////////////////////////////////////Kraj Enk6
    }