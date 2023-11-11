using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
public class VariblesGlobalText : MonoBehaviour
{
	// Token: 0x0600027D RID: 637 RVA: 0x00010A60 File Offset: 0x0000EC60
	private void Start()
	{
		VariblesGlobalText.Text1 = "No more bullets.";
		VariblesGlobalText.Text2 = "You picked up the stun gun ammunition.";
		VariblesGlobalText.Text3 = "You picked up the stun gun. It can knock a man out for a short time.";
		VariblesGlobalText.Text4 = "You can not hide when you are within the olds' eyesight.";
		VariblesGlobalText.Text5 = "Find the car wheels.";
		VariblesGlobalText.Text6 = "You need petrol.";
		VariblesGlobalText.Text7 = "You need to find the car keys.";
		VariblesGlobalText.Text8 = "You need a saw to cut the board.";
		VariblesGlobalText.Text9 = "You found the first part of the stun gun.";
		VariblesGlobalText.Text10 = "You found the second part of the stun gun. Assemble it it on the workbench.";
		VariblesGlobalText.Text11 = "Find two parts of the gun and assemble them on the workbench.";
		VariblesGlobalText.Text12 = "You need something sharp to break the safe. \nA screwdriver will do.";
		VariblesGlobalText.Text13 = "Click on the cabinet door to hide in.";
		VariblesGlobalText.Text14 = "It seems that this car can be repaired. Click on it.";
		VariblesGlobalText.Text15 = "This wall can be broken. Find a suitable tool.";
		VariblesGlobalText.Text16 = "It is necessary to repair the car.";
		VariblesGlobalText.Text17 = "You need a turnscrew.";
		VariblesGlobalText.Text18 = "You need a tool to break it.";
		VariblesGlobalText.Text19 = "Workbench";
		VariblesGlobalText.Text20 = "You need a crowbar.";
		VariblesGlobalText.Text21 = "I can break the safe with a help of it.";
		VariblesGlobalText.Text22 = "You need a board to repair the floor.";
		VariblesGlobalText.Text23 = "Video is not ready.";
		VariblesGlobalText.Text24 = "Sensibility";
		VariblesGlobalText.Text25 = "START";
		VariblesGlobalText.Text26 = "SETTINGS";
		VariblesGlobalText.Text27 = "EXIT";
		VariblesGlobalText.Text28 = "It is a safe zone. Grandpa and granny can not get here.";
		VariblesGlobalText.NO_translate1 = "The world is a dangerous place to live, \nnot because of the people who are evil,\n but because of the people who don't \ndo anything about it.";
		VariblesGlobalText.NO_translate2 = "Remember that all through history, \nthere have been tyrants and \nmurderers, and for a time, they \nseem invincible.But in the end, \nthey always fall.Always.";
		VariblesGlobalText.Text29 = "WIN!";
		VariblesGlobalText.Text30 = "RATE IT!";
		VariblesGlobalText.Text31 = "MAIN MENU";
		VariblesGlobalText.Text32 = "Close";
		VariblesGlobalText.Text33 = "You are in a safe area. \nPlease give 1 minute. \nDo you like to play?";
		VariblesGlobalText.Text34 = "Yes, I liked it.";
		VariblesGlobalText.Text35 = "Did not like.";
		VariblesGlobalText.Text43 = "Game mode.";
		VariblesGlobalText.Text36 = "Went\n fishing";
		VariblesGlobalText.Text37 = "Ghost";
		VariblesGlobalText.Text38 = "Classic";
		VariblesGlobalText.Text39 = "Heavy";
		VariblesGlobalText.Text40 = "You are a ghost. No one can see you now.";
		VariblesGlobalText.Text41 = "Classic game mode.";
		VariblesGlobalText.Text42 = "You have only one attempt. \n Granny and grandpa are faster.";
		VariblesGlobalText.Text44 = "You need a hammer and nails ,\n to retair the ladder.";
		VariblesGlobalText.Text45 = "You picked up nails.";
		VariblesGlobalText.Text46 = "Need a paddle.\n I can't sail away without it.";
		VariblesGlobalText.Text47 = "Village\n PANSELHILL";
		VariblesGlobalText.Text48 = "We are not waiting for you!";
		VariblesGlobalText.Text49 = "Need a knife to cut.";
		VariblesGlobalText.Text50 = "Try to deenergize the door.\n You need to follow the wires.";
		VariblesGlobalText.Text51 = "I have some time\n while the dog is eating meat.";
		VariblesGlobalText.Text52 = "MY MOM\n AND DAD";
		VariblesGlobalText.Text53 = "I managed to escape from crazy olds.\n But where shall I go, and where am I?";
		VariblesGlobalText.Text54 = "A small town is up ahead. I hope, there are police to help me.";
		VariblesGlobalText.Text55 = "Easy";
		VariblesGlobalText.Text56 = "Easy mode. Grandma and grandpa are very slow.";
		VariblesGlobalText.Text57 = "You found an energy drink. Drink increases the speed.";
		VariblesGlobalText.Text58 = "Escape by car.";
		VariblesGlobalText.Text59 = "Escape by boat.";
		VariblesGlobalText.Text60 = "Escape over the bridge.";
		VariblesGlobalText.Text61 = "Turn on the radio.";
		if (VariblesGlobal.SelectLanguage == "")
		{
			this.SelectLanguage = Application.systemLanguage.ToString();
		}
		else
		{
			this.SelectLanguage = VariblesGlobal.SelectLanguage;
		}
		string selectLanguage = this.SelectLanguage;
		if (selectLanguage == "Russian")
		{
			VariblesGlobalText.Text1 = "Закончились патроны.";
			VariblesGlobalText.Text2 = "Вы подобрали боеприпасы к электрошокеру.";
			VariblesGlobalText.Text3 = "Вы подобрали электрошокер. Он отключает человека на короткое время.";
			VariblesGlobalText.Text4 = "Нельзя прятаться, когда вы в поле зрения страриков.";
			VariblesGlobalText.Text5 = "Найдите колеса от машины.";
			VariblesGlobalText.Text6 = "Нужен бензин.";
			VariblesGlobalText.Text7 = "Нужно найти ключи от машины.";
			VariblesGlobalText.Text8 = "Нужна пила, чтобы пилить доску.";
			VariblesGlobalText.Text9 = "Нашли одну часть пистолета электрошокера.";
			VariblesGlobalText.Text10 = "Нашли вторую часть пистолета. Соберите его на верстаке.";
			VariblesGlobalText.Text11 = "Найдите две части пистолета и соберите на верстаке.";
			VariblesGlobalText.Text12 = "Нужно что-то острое, чтобы взломать сейф. \nСойдет и отвертка.";
			VariblesGlobalText.Text13 = "Кликните по двери шкафа, чтобы спрятаться.";
			VariblesGlobalText.Text14 = "Кажется эту машину можно починить. Кликните по ней.";
			VariblesGlobalText.Text15 = "Эту стену можно сломать. Найдите подходящий инструмент.";
			VariblesGlobalText.Text16 = "Это нужно, чтобы починить машину.";
			VariblesGlobalText.Text17 = "Нужен гаечный ключ.";
			VariblesGlobalText.Text18 = "Нужен инструмент, чтобы сломать.";
			VariblesGlobalText.Text19 = "Верстак";
			VariblesGlobalText.Text20 = "Нужен лом";
			VariblesGlobalText.Text21 = "С помощью нее я могу взломать сейф.";
			VariblesGlobalText.Text22 = "Нужна доска, чтобы починить пол.";
			VariblesGlobalText.Text23 = "Видео не готово.";
			VariblesGlobalText.Text24 = "Чувствительность";
			VariblesGlobalText.Text25 = "СТАРТ";
			VariblesGlobalText.Text26 = "НАСТРОЙКИ";
			VariblesGlobalText.Text27 = "ВЫХОД";
			VariblesGlobalText.Text28 = "Это безопасная зона. Старики сюда не доберутся.";
			VariblesGlobalText.NO_translate1 = "Мир - опасное место для жизни не из-за \nзлых людей, а из-за людей, которые ничего\n с этим не делают.";
			VariblesGlobalText.NO_translate2 = "Помните, что на протяжении всей истории \nбыли тираны и убийцы, и какое-то время  \nони казались непобедимыми. Но в конце \nони всегда падают. Всегда.";
			VariblesGlobalText.Text29 = "ПОБЕДА!";
			VariblesGlobalText.Text30 = "ОЦЕНИТЬ!";
			VariblesGlobalText.Text31 = "НА ГЛАВНУЮ";
			VariblesGlobalText.Text32 = "Закрыть";
			VariblesGlobalText.Text33 = "Вы находитесь в безопасной зоне. \nУделите ,пожалуйста , 1 минуту. \nНравиться ли вам играть?";
			VariblesGlobalText.Text34 = "Да, понравилось.";
			VariblesGlobalText.Text35 = "Не понравилось.";
			VariblesGlobalText.Text43 = "Режим игры.";
			VariblesGlobalText.Text36 = "Ушел на\nрыбалку";
			VariblesGlobalText.Text37 = "Призрак";
			VariblesGlobalText.Text38 = "Классика";
			VariblesGlobalText.Text39 = "Тяжелый";
			VariblesGlobalText.Text40 = "Вы призрак. Вас никто не видит.";
			VariblesGlobalText.Text41 = "Классический режим игры.";
			VariblesGlobalText.Text42 = "У вас есть одна попытка.\nБабка и дед быстрее.";
			VariblesGlobalText.Text44 = "Нужен молоток и гвозди\n что бы починить лестницу.";
			VariblesGlobalText.Text45 = "Вы подобрали гвозди.";
			VariblesGlobalText.Text46 = "Нужно весло.\n Без него я не могу уплыть.";
			VariblesGlobalText.Text47 = "Деревня\n PANSELHILL";
			VariblesGlobalText.Text48 = "Мы вас не ждем!";
			VariblesGlobalText.Text49 = "Нужен нож чтобы резать.";
			VariblesGlobalText.Text50 = "Мне надо попробовать обесточить\n дверь. Куда ведет провод на столбах?";
			VariblesGlobalText.Text51 = "У меня есть немного времени\n пока собака ест мясо.";
			VariblesGlobalText.Text52 = "МОИ МАМА\n И ПАПА";
			VariblesGlobalText.Text53 = "Мне удалось сбежать от сумашедих стариков.\n Но куда идти, где я?";
			VariblesGlobalText.Text54 = "Впереди маленький городок. Надеюсь, там есть полиция и мне помогут...";
			VariblesGlobalText.Text55 = "Легко";
			VariblesGlobalText.Text56 = "Легкий режим. Бабка и дед очень медленные.";
			VariblesGlobalText.Text57 = "Вы нашли энергетический напиток. Напиток увеличивает скорость.";
			VariblesGlobalText.Text58 = "Сбежать на машине.";
			VariblesGlobalText.Text59 = "Сбежать на лодке.";
			VariblesGlobalText.Text60 = "Сбежать через мост.";
			VariblesGlobalText.Text61 = "Включить радио.";
			return;
		}
		if (selectLanguage == "German")
		{
			VariblesGlobalText.Text1 = "Keine Munition mehr.";
			VariblesGlobalText.Text2 = "Du hast die Munition für die Elektroschocker gesammelt.";
			VariblesGlobalText.Text3 = "Du hast die Elektroschocker aufgehoben. Es schaltet den Menschen für kurze Zeit aus..";
			VariblesGlobalText.Text4 = "Du kannst dich nicht verstecken, wenn dich die alten sehen.";
			VariblesGlobalText.Text5 = "Finden Sie die Räder vom Auto.";
			VariblesGlobalText.Text6 = "Wir brauchen Benzin.";
			VariblesGlobalText.Text7 = "Wir müssen die Autoschlüssel finden.";
			VariblesGlobalText.Text8 = "Brauchen eine Säge, um das Brett zu sägen.";
			VariblesGlobalText.Text9 = "Einen Teil der Elektroschocker gefunden.";
			VariblesGlobalText.Text10 = "Gefunden den zweiten Teil der Waffe. Montieren Sie es auf der Werkbank.";
			VariblesGlobalText.Text11 = "Найдите две части пистолета и соберите на верстаке.";
			VariblesGlobalText.Text12 = "Brauchen etwas Spitzes, um den Safe zu knacken. \nPasst auch ein Schraubendreher.";
			VariblesGlobalText.Text13 = "Klicken Sie auf die Schranktür, um sich zu verstecken.";
			VariblesGlobalText.Text14 = "Sieht so aus, als ob dieses Auto repariert werden kann. Klicken Sie drauf.";
			VariblesGlobalText.Text15 = "Diese Wand kann durchbrochen werden. Finden Sie das richtige Werkzeug.";
			VariblesGlobalText.Text16 = "Das brauchen wir, um ein Auto zu reparieren..";
			VariblesGlobalText.Text17 = "Brauche einen Schraubenschlüssel.";
			VariblesGlobalText.Text18 = "Brauchen ein Werkzeug zum Zerbrechen.";
			VariblesGlobalText.Text19 = "Werkbank";
			VariblesGlobalText.Text20 = "Brauche ein Brecheisen.";
			VariblesGlobalText.Text21 = "Mit ihre Hilfe kann ich den Safe knacken.";
			VariblesGlobalText.Text22 = "Brauche ein Brett, um den Fußboden zu reparieren..";
			VariblesGlobalText.Text23 = "Video ist nicht fertig.";
			VariblesGlobalText.Text24 = "Empfindlichkeit";
			VariblesGlobalText.Text25 = "START";
			VariblesGlobalText.Text26 = "EISTELLUNGEN";
			VariblesGlobalText.Text27 = "AUSGANG";
			VariblesGlobalText.Text28 = "Eine sichere Zone. Alte Leute werden nicht hierher kommen.";
			VariblesGlobalText.NO_translate1 = "Welt - kein gefährlicher Ort zum Leben, nicht wegen \nböse Menschen aber wegen der Menschen die nichts\n mit dem machen.";
			VariblesGlobalText.NO_translate2 = "Denke daran, im Laufe der ganzen Geschichte \nwaren Tyrannen und Mörder, und für eine Weile  \n schienen sie unbesiegbar.  Aber am Ende \nfallen die immer. Immer.";
			VariblesGlobalText.Text29 = "GEWONNEN!";
			VariblesGlobalText.Text30 = "BEWERTUNG!";
			VariblesGlobalText.Text31 = "HAUPTSEITE";
			VariblesGlobalText.Text32 = "SCHLIESSEN";
			VariblesGlobalText.Text33 = "Sie sind in der sicheren Zone. \nGeben ,Sie bitte , 1 Minute. \nMacht es Spaß zu spielen?";
			VariblesGlobalText.Text34 = "Ja, zufrieden.";
			VariblesGlobalText.Text35 = "Unzufrieden.";
			VariblesGlobalText.Text43 = "Spielmodus.";
			VariblesGlobalText.Text36 = "Weg gegangen zum\nfischen";
			VariblesGlobalText.Text37 = "Geist";
			VariblesGlobalText.Text38 = "Klassisch";
			VariblesGlobalText.Text39 = "Schwer";
			VariblesGlobalText.Text40 = "Sie sind ein Geist. Für alle Unsichtbar.";
			VariblesGlobalText.Text41 = "Klassische Spielmodus.";
			VariblesGlobalText.Text42 = "Sie haben nur eine Chance.\nOma und Opa sind schneller.";
			VariblesGlobalText.Text44 = "Ich brauche einen Hammer und Nägel\n, um die Treppe zu reparieren.";
			VariblesGlobalText.Text45 = "Sie haben Nägeln aufgehoben.";
			VariblesGlobalText.Text46 = "Ich brauche ein Paddel.\n Ohne es kann ich nicht weg schwimmen.";
			VariblesGlobalText.Text47 = "Dorf\n PANSELHILL";
			VariblesGlobalText.Text48 = "Wir warten auch euch nicht!";
			VariblesGlobalText.Text49 = "Brauche Messer um zu schneiden.";
			VariblesGlobalText.Text50 = "Ich muss versuchen stromlos zumachen die\n Türe. Wohin führt der Draht auf dem Posten?";
			VariblesGlobalText.Text51 = "Ich habe ein wenig Zeit \n Bis der Hund Fleisch frisst.";
			VariblesGlobalText.Text52 = "MEINE MAMA\n UND PAPA";
			VariblesGlobalText.Text53 = "Ich habe es geschafft von den alten weg zu kommen.\n Und jetzt wohin, wo bin ich?";
			VariblesGlobalText.Text54 = "Vorne ist ein kleines Stadt. Hoffe, da ist die Polizei und mir wird geholfen...";
			VariblesGlobalText.Text55 = "Leicht";
			VariblesGlobalText.Text56 = "Leichtes Spielmodus. Oma und Opa sind sehr langsam.";
			return;
		}
		if (selectLanguage == "Turkish")
		{
			VariblesGlobalText.Text1 = "Daha fazla mühimmat yok.";
			VariblesGlobalText.Text2 = "Sersemletici silahlar için mühimmat topladın.";
			VariblesGlobalText.Text3 = "Şok tabancası aldın. İnsanları kısa bir süre için kapatır.";
			VariblesGlobalText.Text4 = "Yaşlılar seni görünce saklanamazsın.";
			VariblesGlobalText.Text5 = "Arabadan tekerlekleri bulun.";
			VariblesGlobalText.Text6 = "Benzine ihtiyacımız var.";
			VariblesGlobalText.Text7 = "Arabanın anahtarlarını bulmalıyız.";
			VariblesGlobalText.Text8 = "Tahtayı görmek için testere gerekir.";
			VariblesGlobalText.Text9 = "Şok tabancalarının bir kısmını buldum.";
			VariblesGlobalText.Text10 = "Silahın ikinci kısmını buldum. Tezgah üzerine monte.";
			VariblesGlobalText.Text11 = "Tabancanın iki parçasını bulun ve tezgah üzerinde toplayın.";
			VariblesGlobalText.Text12 = "Kasayı kırmak için biraz dantel lazım. \nAyrıca bir tornavidaya uyar.";
			VariblesGlobalText.Text13 = "Gizlemek için kabin kapısını tıklayın.";
			VariblesGlobalText.Text14 = "Görünüşe göre bu araba tamir edilebilir. Üzerine tıklayın.";
			VariblesGlobalText.Text15 = "Bu duvar kırılabilir. Doğru aracı bulun.";
			VariblesGlobalText.Text16 = "Bir arabayı tamir etmek için ona ihtiyacımız var.";
			VariblesGlobalText.Text17 = "İngiliz anahtarı lazım.";
			VariblesGlobalText.Text18 = "Kırmak için bir araç ihtiyacınız var.";
			VariblesGlobalText.Text19 = "Tezgâh";
			VariblesGlobalText.Text20 = "Levye gerekir.";
			VariblesGlobalText.Text21 = "Onların yardımı ile kasayı kırabilirim.";
			VariblesGlobalText.Text22 = "Zemini düzeltmek için tahtaya ihtiyacınız var.";
			VariblesGlobalText.Text23 = "Video bitmedi.";
			VariblesGlobalText.Text24 = "duyarlılık";
			VariblesGlobalText.Text25 = "BAŞLANGIÇ";
			VariblesGlobalText.Text26 = "AYARLAR";
			VariblesGlobalText.Text27 = "CUSTOM";
			VariblesGlobalText.Text28 = "Güvenli bir bölge. Yaşlı insanlar buraya gelmeyecek.";
			VariblesGlobalText.NO_translate1 = "Dünya - yaşanacak tehlikeli bir yer değil, yüzünden değil \nKötü insanlar ama insanlar yüzünden hiçbir şey\n bununla yapmak.";
			VariblesGlobalText.NO_translate2 = "Unutma, hikaye boyunca \nzorbalar ve katillerdi, ve bir süre  \n yenilmez görünüyorlardı.  Ama sonunda \nher zaman düşer. her zaman.";
			VariblesGlobalText.Text29 = "KAZANDIN!";
			VariblesGlobalText.Text30 = "DEĞERLENDİRME!";
			VariblesGlobalText.Text31 = "ANA";
			VariblesGlobalText.Text32 = "KAPAT";
			VariblesGlobalText.Text33 = "Güvenli bölgedesiniz. \nverme ,Sen lütfen , 1 dakika. \nOynamak eğlenceli?";
			VariblesGlobalText.Text34 = "Evet iyiyim.";
			VariblesGlobalText.Text35 = "Hayır, beğenmedim.";
			VariblesGlobalText.Text43 = "Oyun modu.";
			VariblesGlobalText.Text36 = "Uzağa gitti\nbalık";
			VariblesGlobalText.Text37 = "Ruh";
			VariblesGlobalText.Text38 = "Klasik";
			VariblesGlobalText.Text39 = "Zor";
			VariblesGlobalText.Text40 = "Sen bir hayalet. Tüm görünmez.";
			VariblesGlobalText.Text41 = "Klasik oyun modu.";
			VariblesGlobalText.Text42 = "Sadece bir şansın var.\nBüyükanne ve büyükbaba daha hızlı.";
			VariblesGlobalText.Text44 = "Çekiç ve çiviye ihtiyacım var\n, merdivenleri tamir etmek.";
			VariblesGlobalText.Text45 = "Tırnak çektirdiler.";
			VariblesGlobalText.Text46 = "Bir rakete ihtiyacım var.\n Onsuz yüzemem.";
			VariblesGlobalText.Text47 = "Köy\n PANSELHILL";
			VariblesGlobalText.Text48 = "Seni beklemiyoruz!";
			VariblesGlobalText.Text49 = "Kesmek için bıçak gerekir.";
			VariblesGlobalText.Text50 = "Gücü kapatmaya çalışmalıyım\n kapi. Tel direk nereye gidiyor??";
			VariblesGlobalText.Text51 = "Biraz zamanım var \n Köpek et yiyene kadar.";
			VariblesGlobalText.Text52 = "BENIM ANNE\n VE PAPA";
			VariblesGlobalText.Text53 = "Eskilerinden uzak durmayı başardım.\n Ve şimdi nerede, neredeyim?";
			VariblesGlobalText.Text54 = "Ön küçük bir kasabadır. Umarım polis vardır ve yardım ederim...";
			VariblesGlobalText.Text55 = "Kolay";
			VariblesGlobalText.Text56 = "Kolay oyun modu. Büyükanne ve büyükbaba çok yavaş.";
			return;
		}
		if (selectLanguage == "Spanish")
		{
			VariblesGlobalText.Text1 = "Los cartuchos han acabado.";
			VariblesGlobalText.Text2 = "Ha recojido la munición del táser.";
			VariblesGlobalText.Text3 = "Ha recojido el táser. Él paraliza a una persona por un tiempo corto.";
			VariblesGlobalText.Text4 = "No puede esconderse cuando está a la vista de los ancianos.";
			VariblesGlobalText.Text5 = "Encuentra las ruedas del coche.";
			VariblesGlobalText.Text6 = "Necesita gasolina.";
			VariblesGlobalText.Text7 = "Necesita encontrar las llaves del coche.";
			VariblesGlobalText.Text8 = "Necesita una sierra para cortar el tablero.";
			VariblesGlobalText.Text9 = "Ha encontrado una parte de táser.";
			VariblesGlobalText.Text10 = "He encontrado segunda parte de táser. Creelo en la mesa de trabajo.";
			VariblesGlobalText.Text11 = "Encuentre dos partes de la pistola y creelo en la mesa de trabajo.";
			VariblesGlobalText.Text12 = "Necesita encontrar algo afilado para romper la caja fuerte. Pues ven el destornillador.";
			VariblesGlobalText.Text13 = "Haga clic en la puerta del armario para esconderse.";
			VariblesGlobalText.Text14 = "Parece que este coche puede ser reparada. Haga clic en él.";
			VariblesGlobalText.Text15 = "Esta pared se puede romper. Encuentre la herramienta adecuada.";
			VariblesGlobalText.Text16 = "Lo es necesario para arreglar el coche.";
			VariblesGlobalText.Text17 = "Necesita un llave inglesa.";
			VariblesGlobalText.Text18 = "Necesita una herramienta para romperlo.";
			VariblesGlobalText.Text19 = "Mesa de trabajo";
			VariblesGlobalText.Text20 = "Necesita una chatarra";
			VariblesGlobalText.Text21 = "Si usarla, puedo romper la caja fuerte.";
			VariblesGlobalText.Text22 = "Necesita un tablero para reparar el piso.";
			VariblesGlobalText.Text23 = "El video no está listo.";
			VariblesGlobalText.Text24 = "Sensibilidad";
			VariblesGlobalText.Text25 = "ARRANQUE";
			VariblesGlobalText.Text26 = "CONFIGURACÍON";
			VariblesGlobalText.Text27 = "SALIDA";
			VariblesGlobalText.Text28 = "Es una zona de seguridad. Los ancianos no vienen aquí.";
			VariblesGlobalText.Text29 = "¡VICTORIA!";
			VariblesGlobalText.Text30 = "EVALUAR!";
			VariblesGlobalText.Text31 = "A la página PRINCIPAL.";
			VariblesGlobalText.Text32 = "Cerrar";
			VariblesGlobalText.Text33 = "Usted está en la zona seguridad. Por favor dejanos 1 minuta. Y Usted, ¿Les gusta jugar en éste juego?";
			VariblesGlobalText.Text34 = "Sí, a mí me lo gustó.";
			VariblesGlobalText.Text35 = "No, a mí no lo me gustó.";
			VariblesGlobalText.Text43 = "Dificultad del juego.";
			VariblesGlobalText.Text36 = "Fui a pescar.";
			VariblesGlobalText.Text37 = "Un fantasma";
			VariblesGlobalText.Text38 = "Clásico";
			VariblesGlobalText.Text39 = "Dificíl";
			VariblesGlobalText.Text40 = "Es un fantasma. Nadie se ve.";
			VariblesGlobalText.Text41 = "Modo de juego Clásico.";
			VariblesGlobalText.Text42 = "Tienes un intento. El abuelo y el abuela son más rápidos.";
			VariblesGlobalText.Text44 = "Necesita un martillo y unos clavos para arreglar la escalera.";
			VariblesGlobalText.Text45 = "Ha recojido los clavos.";
			VariblesGlobalText.Text46 = "Necesita una paleta. Sin él, no puedo navegar.";
			VariblesGlobalText.Text47 = "Pueblo \nPANSELHILL";
			VariblesGlobalText.Text48 = "No Le esperamos!";
			VariblesGlobalText.Text49 = "Necesita un cuchillo para cortar.";
			VariblesGlobalText.Text50 = "Tengo que tratar de apretar la puerta. ¿Dónde lleva el cable en los postes?";
			VariblesGlobalText.Text51 = "Tengo un poco de tiempo mientras el perro come el carne.";
			VariblesGlobalText.Text52 = "Son mi mama\n y papa";
			VariblesGlobalText.Text53 = "Logré escapar de los viejos locos. Pero, ¿dónde tengo que ir, dónde estoy?";
			VariblesGlobalText.Text54 = "Hay una pequeña ciudad por delante. Espero que haya policía y me ayudan...";
			VariblesGlobalText.Text55 = "Fácil";
			VariblesGlobalText.Text56 = "Modo fácil. Abuela y abuelo son muy lentos.";
			return;
		}
		if (!(selectLanguage == "Japanese"))
		{
			return;
		}
		VariblesGlobalText.Text1 = "弾薬が終わりました。";
		VariblesGlobalText.Text2 = "スタンガンの弾薬が拾われました。";
		VariblesGlobalText.Text3 = "スタンガンが拾われました。これは短い時間に人間をノックアウトします。";
		VariblesGlobalText.Text4 = "老人で見られるうちに、あなたが隠れられません。";
		VariblesGlobalText.Text5 = "車の車輪を見つけてください。";
		VariblesGlobalText.Text6 = "ガソリンが必要です。";
		VariblesGlobalText.Text7 = "車のキーを見つけてください。";
		VariblesGlobalText.Text8 = "板を切るように、鋸が必要です。";
		VariblesGlobalText.Text9 = "スタンガンの一部を見つけました。";
		VariblesGlobalText.Text10 = "スタンガンの二つ目の部を見つけました。ガンを作業台でクラフトしてください。";
		VariblesGlobalText.Text11 = "スタンガンの二つ部を見つけて、作業台でクラフトしてください。";
		VariblesGlobalText.Text12 = "金庫を破るように何か鋭いものが必要です。";
		VariblesGlobalText.Text13 = "自分を隠すように、戸棚のドアにクリックしてください。";
		VariblesGlobalText.Text14 = "その車を修理することができるようです。車にクリックしてください。";
		VariblesGlobalText.Text15 = "その壁を壊すことができます。適当な道具を見つけてください。";
		VariblesGlobalText.Text16 = "それは車を修理するための物です。";
		VariblesGlobalText.Text17 = "レンチが必要です。";
		VariblesGlobalText.Text18 = "破るように道具が必要です。";
		VariblesGlobalText.Text19 = "作業台";
		VariblesGlobalText.Text20 = "バールが必要です。";
		VariblesGlobalText.Text21 = "それを使って金庫を破ることができます。";
		VariblesGlobalText.Text22 = "床を修理するために板が必要です。";
		VariblesGlobalText.Text23 = "ビデオがまだできていません。";
		VariblesGlobalText.Text24 = "感度";
		VariblesGlobalText.Text25 = "スタート";
		VariblesGlobalText.Text26 = "設定";
		VariblesGlobalText.Text27 = "終了";
		VariblesGlobalText.Text28 = "これは安全な地帯です。老人たちがここまで達することができません。";
		VariblesGlobalText.Text36 = "釣りに\n行った";
		VariblesGlobalText.Text37 = "幽霊";
		VariblesGlobalText.Text38 = "クラシック";
		VariblesGlobalText.Text39 = "ハード";
		VariblesGlobalText.Text40 = "あなたは幽霊です。あなたが誰もに目に見えません。";
		VariblesGlobalText.Text41 = "クラシックゲームモード。";
		VariblesGlobalText.Text42 = "あなたが一つのチャンスがある。\nお祖母ちゃんとお祖父ちゃんがあなたより早いです。";
		VariblesGlobalText.Text44 = "階段を修理するために、\nハンマーと釘が必要です。";
		VariblesGlobalText.Text45 = "釘が拾われました。";
		VariblesGlobalText.Text46 = "オールが必要です。\nオールがなければ泳ぎ去れません。";
		VariblesGlobalText.Text47 = "村\nパンセルヒル";
		VariblesGlobalText.Text48 = "我々はあなたたちを待っていないよ！";
		VariblesGlobalText.Text49 = "切るようにナイフが必要です。";
		VariblesGlobalText.Text50 = "ドアを無効に\nしてみるのは必要です。";
		VariblesGlobalText.Text51 = "犬が肉を食べているうちに、\n少し時間がある。";
		VariblesGlobalText.Text52 = "私の母と\n父です。";
		VariblesGlobalText.Text53 = "狂った老人たちからなんとか逃げたなあ。\n一体どこへ行くのか？私がどこでいるの？";
		VariblesGlobalText.Text54 = "まっすぐは小さな町です。あそこで警察があったらいい、私に助けられたらいいなあ。";
		VariblesGlobalText.Text55 = "イージー";
		VariblesGlobalText.Text56 = "イージーモード。お祖母ちゃんとお祖父ちゃんは大変に遅いです。";
	}

	// Token: 0x040002B5 RID: 693
	public static string Text1;

	// Token: 0x040002B6 RID: 694
	public static string Text2;

	// Token: 0x040002B7 RID: 695
	public static string Text3;

	// Token: 0x040002B8 RID: 696
	public static string Text4;

	// Token: 0x040002B9 RID: 697
	public static string Text5;

	// Token: 0x040002BA RID: 698
	public static string Text6;

	// Token: 0x040002BB RID: 699
	public static string Text7;

	// Token: 0x040002BC RID: 700
	public static string Text8;

	// Token: 0x040002BD RID: 701
	public static string Text9;

	// Token: 0x040002BE RID: 702
	public static string Text10;

	// Token: 0x040002BF RID: 703
	public static string Text11;

	// Token: 0x040002C0 RID: 704
	public static string Text12;

	// Token: 0x040002C1 RID: 705
	public static string Text13;

	// Token: 0x040002C2 RID: 706
	public static string Text14;

	// Token: 0x040002C3 RID: 707
	public static string Text15;

	// Token: 0x040002C4 RID: 708
	public static string Text16;

	// Token: 0x040002C5 RID: 709
	public static string Text17;

	// Token: 0x040002C6 RID: 710
	public static string Text18;

	// Token: 0x040002C7 RID: 711
	public static string Text19;

	// Token: 0x040002C8 RID: 712
	public static string Text20;

	// Token: 0x040002C9 RID: 713
	public static string Text21;

	// Token: 0x040002CA RID: 714
	public static string Text22;

	// Token: 0x040002CB RID: 715
	public static string Text23;

	// Token: 0x040002CC RID: 716
	public static string Text24;

	// Token: 0x040002CD RID: 717
	public static string Text25;

	// Token: 0x040002CE RID: 718
	public static string Text26;

	// Token: 0x040002CF RID: 719
	public static string Text27;

	// Token: 0x040002D0 RID: 720
	public static string Text28;

	// Token: 0x040002D1 RID: 721
	public static string Text29;

	// Token: 0x040002D2 RID: 722
	public static string Text30;

	// Token: 0x040002D3 RID: 723
	public static string Text31;

	// Token: 0x040002D4 RID: 724
	public static string Text32;

	// Token: 0x040002D5 RID: 725
	public static string Text33;

	// Token: 0x040002D6 RID: 726
	public static string Text34;

	// Token: 0x040002D7 RID: 727
	public static string Text35;

	// Token: 0x040002D8 RID: 728
	public static string Text36;

	// Token: 0x040002D9 RID: 729
	public static string Text37;

	// Token: 0x040002DA RID: 730
	public static string Text38;

	// Token: 0x040002DB RID: 731
	public static string Text39;

	// Token: 0x040002DC RID: 732
	public static string Text40;

	// Token: 0x040002DD RID: 733
	public static string Text41;

	// Token: 0x040002DE RID: 734
	public static string Text42;

	// Token: 0x040002DF RID: 735
	public static string Text43;

	// Token: 0x040002E0 RID: 736
	public static string Text44;

	// Token: 0x040002E1 RID: 737
	public static string Text45;

	// Token: 0x040002E2 RID: 738
	public static string Text46;

	// Token: 0x040002E3 RID: 739
	public static string Text47;

	// Token: 0x040002E4 RID: 740
	public static string Text48;

	// Token: 0x040002E5 RID: 741
	public static string Text49;

	// Token: 0x040002E6 RID: 742
	public static string Text50;

	// Token: 0x040002E7 RID: 743
	public static string Text51;

	// Token: 0x040002E8 RID: 744
	public static string Text52;

	// Token: 0x040002E9 RID: 745
	public static string Text53;

	// Token: 0x040002EA RID: 746
	public static string Text54;

	// Token: 0x040002EB RID: 747
	public static string Text55;

	// Token: 0x040002EC RID: 748
	public static string Text56;

	// Token: 0x040002ED RID: 749
	public static string Text57;

	// Token: 0x040002EE RID: 750
	public static string Text58;

	// Token: 0x040002EF RID: 751
	public static string Text59;

	// Token: 0x040002F0 RID: 752
	public static string Text60;

	// Token: 0x040002F1 RID: 753
	public static string Text61;

	// Token: 0x040002F2 RID: 754
	public static string NO_translate1;

	// Token: 0x040002F3 RID: 755
	public static string NO_translate2;

	// Token: 0x040002F4 RID: 756
	private string SelectLanguage = "";
}
