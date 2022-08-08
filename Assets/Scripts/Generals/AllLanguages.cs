using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllLanguages : MonoBehaviour {

    public static string gameName = "Bus & Subway - Multiplayer Runner";
    public static List<Sprite> listIconLang = new List<Sprite>();
    public static List<Font> listFontLangA = new List<Font>();
    public static List<Font> listFontLangB = new List<Font>();

    public static List<bool> listSupport = new List<bool>()
    {
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true
    };

    public static List<string> listLanguage = new List<string>() { 
        "English",
        "Русский",
        "Français",
        "Deutsche",
        "Español",
        "Português",
        "عربى",
        "日本語",
        "한국어",
        "中文",
        "Tiếng Việt" 
    };

    public static List<string> listLangShort = new List<string>() { 
        "EN",
        "RU",
        "FR",
        "DE",
        "ES",
        "PT",
        "SA",
        "JP",
        "KR",
        "CN",
        "VN" 
    };

    //IN PAGE MENU GAME
    public static List<string> menuWaitMoment = new List<string>() 
    { 
        "Wait a moment...", 
        "Подождите минутку...",
        "Attendez un moment...",
        "Moment mal...", 
        "Espera un momento...", 
        "Espere um momento...", 
        "...انتظر لحظة", 
        "ちょっと 待って...", 
        "잠시 기다립니다...", 
        "稍等片刻...", 
        "Chờ giây lát..." 
    };
    public static List<string> menuTapToPlay = new List<string>() 
    { 
        "Tap to play", 
        "Побежали!",
        "Appuyez pour jouer",
        "Tippen sie spielen", 
        "Toque para jugar", 
        "Toque para jogar", 
        "انقر للعب", 
        "タップして 再生する", 
        "재생하려면 탭하세요", 
        "点击播放", 
        "Táp để chơi" 
    };
    public static List<string> menuGetFree = new List<string>() 
    { 
        "Get free Keys, Hoverboard", 
        "Получить бесплатно Ключи, Hoverboard",
        "Obtenez gratuitement Clés, Hoverboard",
        "Erhalten kostenlose Keys, Hoverboard", 
        "Obtener gratis Llaves, Hoverboard", 
        "Obter chaves Livres, Hoverboard", 
        "الحصول مجانا Keys، Hoverboard", 
        "無料の キーを 入手、Hoverboard", 
        "무료 Keys, Hoverboard 받기", 
        "获得 免费 Keys 和 Hoverboard", 
        "Nhận miễn phí Chìa khóa, Ván bay" 
    };
    public static List<string> menuOn = new List<string>() 
    { 
        "On", 
        "Вкл",
        "Allumé",
        "Ein", 
        "Encendido",
        "Ligado", 
        "تشغيل", 
        "オン", 
        "켜기",
        "开", 
        "Bật" 
    };
    public static List<string> menuOff = new List<string>() 
    { 
        "Off", 
        "Выкл", 
        "Éteint",
        "Aus", 
        "Apagado",
        "Desligado",
        "إيقاف",
        "オフ",
        "끄기",
        "关",
        "Tắt" 
    };
    public static List<string> menuLow = new List<string>() 
    { 
        "Low", 
        "Низко",
        "Bas", 
        "Niedrig",
        "Bajo", 
        "Baixo", 
        "منخفضة",
        "低い", 
        "낮은", 
        "低", 
        "Thấp" 
    };
    public static List<string> menuMedium = new List<string>() 
    { 
        "Medium", 
        "Средне",
        "Moyen",
        "Mittel", 
        "Medio",
        "Médio",
        "متوسطة",
        "媒体", 
        "중간", 
        "中", 
        "Thường" 
    };
    public static List<string> menuHigh = new List<string>() 
    { 
        "High", 
        "Высоко", 
        "Haute",
        "Hoch", 
        "Alto",
        "Alto", 
        "عالية",
        "高い", 
        "높음",
        "高",
        "Cao" 
    };
    public static List<string> menuFriend = new List<string>() 
    { 
        "Friend", 
        "Друг", 
        "Ami",
        "Freund",
        "Amigo",
        "Amigo",
        "صديق",
        "友人", 
        "친구",
        "朋友",
        "Bạn bè" 
    };
    public static List<string> menuCountry = new List<string>() 
    { 
        "Country", 
        "Страна", 
        "Pays",
        "Land",
        "País",
        "País",
        "بلد",
        "国",
        "국가",
        "国家",
        "Trong nước" 
    };
    public static List<string> menuApply = new List<string>() 
    { 
        "Apply", 
        "Применить",
        "Appliquer", 
        "Bewerben",
        "Aplicar", 
        "Aplique", 
        "تطبيق", 
        "はい", 
        "대다", 
        "申请", 
        "Xác nhận" 
    };
    public static List<string> menuCancel = new List<string>() 
    { 
        "Cancel", 
        "Отмена", 
        "Annuler", 
        "Absagen",
        "Cancelar",
        "Cancelar",
        "إلغاء",
        "いいえ", 
        "취소",
        "取消", 
        "Hủy bỏ" 
    };
    public static List<string> menuUpVideo = new List<string>() 
    { 
        "SHARE VIDEO", 
        "ВИДЕО БЕГА", 
        "SHARE VIDEO",
        "HOCHLADEN", 
        "SUBIR VÍDEO", 
        "VÍDEO ACIMA",
        "رفع فيديو",
        "アップロード", 
        "비디오 업로드",
        "分享视频", 
        "TẢI LÊN" 
    };
    public static List<string> menuMissions = new List<string>() 
    { 
        "MISSIONS", 
        "МИССИИ",
        "MISSIONS",
        "MISSIONEN",
        "MISIONES",
        "MISSÕES", 
        "البعثات",
        "ミッション", 
        "임무", 
        "使命", 
        "NHIỆM VỤ" 
    };
    public static List<string> menuChallenge = new List<string>() 
    { 
        "CHALLENGE", 
        "ЧЕЛЛЕНДЖ", 
        "CONTESTER",
        "ANFECHTEN", 
        "DESAFÍO", 
        "DESAFIO", 
        "التحدي",
        "チャレンジ", 
        "도전",
        "挑战", 
        "THỬ THÁCH" 
    };
    public static List<string> menuTitleMissions = new List<string>() 
    { 
        "Collect items", 
        "Собери предметы",
        "Recueillir des objets",
        "Artikel sammeln",
        "Recoge objetos", 
        "Coletar itens",
        "جمع العناصر", 
        "アイテムを 収集する",
        "항목을 수집", 
        "收集物品",
        "Thu thập vật phẩm" 
    };
    public static List<string> menuTitleChallenge = new List<string>() 
    { 
        "Collect letters", 
        "Собери буквы",
        "Recueillir des lettres",
        "Briefe sammeln", 
        "Recoge cartas",
        "Coletar cartas",
        "جمع حروف", 
        "手紙を 集める",
        "편지 수집",
        "收集信件", 
        "Thu thập chữ cái" 
    };
    public static List<string> menuNoMissions = new List<string>() 
    { 
        "No missions", 
        "Нет миссий", 
        "Aucune mission",
        "Keine missionen",
        "Sin misiones",
        "Sem missões",
        "لا البعثات", 
        "ません ミッション", 
        "임무 없음", 
        "没有任务", 
        "Không có nhiệm vụ" 
    };
    public static List<string> menuNoChallenge = new List<string>() 
    { 
        "No challenge", 
        "Нет челленджов", 
        "Aucun contester",
        "Keine anfechten",
        "Sin desafío", 
        "Sem desafio", 
        "لا التحدي",
        "ません チャレンジ", 
        "도전 없음", 
        "没有挑战",
        "Không có thử thách" 
    };
    public static List<string> menuNoteMissions = new List<string>() 
    { 
        "While running, you will see different items on your way. Pick them up, if you collect the neccessary number, you will be rewarded with the gift as shown above.", 
        "Во время забега ты увидишь разные предметы, на таоём пути. Возьми их, и если соберёшь необходимое колличество предметов, то получишь достойное вознаграждение, как показано выше.", 
        "En cours d'exécution, vous verrez des éléments étranges affichés sur le chemin. Prenez-les, si vous collectez le nombre total de demandes, vous serez récompensé par le cadeau ci-dessus.", 
        "Während des laufens, werden sie angezeigt seltsame gegenstände auf dem weg sehen. Heben sie sie auf, wenn sie die ganze anzahl von anfragen sammeln, werden sie mit dem geschenk wie oben belohnt.", 
        "Mientras se ejecuta, verá elementos extraños en el camino. Recogerlos, si usted recoge el número completo de solicitudes, usted será recompensado con el regalo como se indica más arriba.",
        "Durante a execução, você verá itens estranhos exibidos no caminho. Pegue-os, se você coletar o número total de pedidos, você será recompensado com o presente, conforme descrito acima.", 
        "أثناء التشغيل، سترى العناصر الغريبة المعروضة على الطريق. التقاط لهم، إذا قمت بجمع العدد الكامل من الطلبات، سوف يكافأ مع هدية على النحو الوارد أعلاه.", 
        "走っている 間、 奇妙な アイテムが 途中で 表示されます。 それらを 拾って、 あなたが 完全な 数の 要求を 集めるならば、 あなたは 上記のような 贈り物で 報われるでしょう。", 
        "실행하는 동안, 당신은 방법에 표시 이상한 항목을 볼 수 있습니다. 그 (것)들을 데리십시오, 당신이 요구의 가득 차있는 수를 모으면, 당신은 위와 같이 선물로 보상 될 것이다.",
        "运行时， 您会看到不 同的项目。 拿起他们， 如果你收集了 必要的数字， 你将得到如上 所示的礼物。", 
        "Trong khi chạy, bạn sẽ nhìn thấy các vật phẩm kỳ lạ xuất hiện trên đường, Hãy nhặt chúng, nếu bạn thu thập đủ số lượng yêu cầu, bạn sẽ nhận được phần thưởng như ở trên." 
    };
    public static List<string> menuNoteChallenge = new List<string>() 
    { 
        "While running, you will see the alphabet shown on the road. Pick them up, if you collect the full letters in the word above, you will be rewarded with a random gift.", 
        "Во время забега на своём пути ты увидишь буквы. Собери все буквы в слове выше, и ты получишь в качестве вознаграждения случайный подарок.", 
        "En cours d'exécution, vous verrez l'alphabet montré sur la route. Prenez-les, si vous collectez les lettres complètes dans le mot ci-dessus, vous serez récompensé par un cadeau aléatoire.", 
        "Während des laufens, werden sie das alphabet auf der straße abgebildet. Hebe sie auf, wenn du die ganzen buchstaben im wort oben sammelst, wirst du mit einem zufälligen geschenk belohnt.",
        "Mientras se ejecuta, verá el alfabeto mostrado en la carretera. Recogerlos, si usted recoge las letras completas en la palabra arriba, usted será recompensado con un regalo al azar.", 
        "Durante a execução, você verá o alfabeto mostrado na estrada. Pegue-os, se você coletar as letras completas na palavra acima, você será recompensado com um presente aleatório.", 
        "أثناء تشغيل، سترى الأبجدية التي تظهر على الطريق. التقاط لهم، إذا كنت جمع الحروف الكاملة في الكلمة أعلاه، سوف يكافأ مع هدية عشوائية.", 
        "走っている 間、 道路に 表示されている アルファベットが 表示されます。 あなたが 上記の 言葉の 完全な 手紙を 集めれば、 あなたは 無作為の 贈り 物で 報われるでしょう。",
        "실행하는 동안, 당신은 도로에 표시된 알파벳을 볼 수 있습니다. 위의 단어 전체 문자를 수집하는 경우를 들고, 당신은 임의의 선물로 보상 될 것이다.", 
        "跑步时， 您会看到道路 上显示的字母。 拿起他们，如果你收集上 面的单词中的 完整的信件， 你会得到一 个随机的礼物。", 
        "Trong khi chạy, bạn sẽ nhìn thấy các chữ cái xuất hiện trên đường, Hãy nhặt chúng, nếu bạn nhặt đủ bộ chữ trong từ ở trên, bạn sẽ nhận được một phần thường ngẫu nhiên." 
    };
    public static List<string> menuTopRun = new List<string>() 
    { 
        "TOP RUN", 
        "ЛИДЕРЫ", 
        "TOP RUN",
        "TOP LAUF",
        "TOP RUN", 
        "TOP RUN", 
        "توب رن",
        "トップラン", 
        "탑 러닝", 
        "最高运行", 
        "XẾP HẠNG" 
    };
    public static List<string> menuHero = new List<string>() 
    { 
        "HERO", 
        "ГЕРОЙ",
        "HÉROS",
        "HELD",
        "HÉROE",
        "HERÓI", 
        "بطل",
        "英雄", 
        "영웅",
        "英雄",
        "TÔI" 
    };
    public static List<string> menuShop = new List<string>() 
    { 
        "SHOP", 
        "МАГАЗ",
        "SHOP",
        "SHOP", 
        "TIENDA",
        "LOJA", 
        "متجر",
        "店舗", 
        "가게", 
        "店",
        "MUA" 
    };
    public static List<string> menuOnline = new List<string>() 
    { 
        "ONLINE", 
        "ОНЛАЙН",
        "EN LIGNE",
        "ONLINE", 
        "EN LÍNEA",
        "ON-LINE", 
        "على الخط",
        "オンライン", 
        "온라인", 
        "线上", 
        "ONLINE" 
    };
    public static List<string> menuPlay = new List<string>() 
    { 
        "PLAY", 
        "ИГРАТЬ",
        "JOUE",
        "SPIELEN",
        "JUGAR",
        "JOGAR",
        "لعب",
        "演じる", 
        "놀이",
        "玩",
        "CHƠI" 
    };
    public static List<string> menuGetHoverboard = new List<string>() 
    { 
        "GET HOVERBOARD", 
        "КУПИТЬ ДОСКУ", 
        "GET HOVERBOARD", 
        "Bekommen HOVERBOARD", 
        "OBTENER HOVERBOARD",
        "OBTER HOVERBOARD", 
        "HOVERBOARD الحصول على", 
        "買う HOVERBOARD", 
        "받기 HOVERBOARD", 
        "得到 HOVERBOARD", 
        "LẤY VÁN BAY" 
    };
    public static List<string> menuGetKeys = new List<string>() 
    { 
        "GET KEYS", 
        "КУПИТЬ КЛЮЧИ",
        "GET CLÉS",
        "Bekommen SCHLÜSSEL", 
        "OBTENER CLAVES", 
        "OBTER CHAVES", 
        "الحصول على المفاتيح", 
        "買う キーズ", 
        "받기 열쇠", 
        "得到 KEYS", 
        "LẤY CHÌA KHÓA" 
    };
    public static List<string> menuShare = new List<string>() 
    { 
        "SHARE", 
        "ПОДЕЛИСЬ",
        "PARTAGER", 
        "AKTIE", 
        "COMPARTIR",
        "COMPARTILHAR", 
        "يتقاسم", 
        "シェア", 
        "공유하기",
        "分享", 
        "CHIA SẺ" 
    };
    public static List<string> menuButtonShare = new List<string>() 
    { 
        "SHARE NOW", 
        "ПОДЕЛИСЬ", 
        "PARTAGER", 
        "AKTIE", 
        "COMPARTIR",
        "COMPARTIR", 
        "شارك الآن", 
        "シェア", 
        "공유하기 지금",
        "现在分享", 
        "CHIA SẺ" 
    };
    public static List<string> menuNoteShare = new List<string>() 
    { 
        "Share this game with your friends on your Facebook page to unlock <color=green>Stela</color> for free.", 
        "Расскажи друзьям об игре на своей странице в Фейсбуке, чтобы бесплатно разблокировать <color=green>Стеллу</color>.", 
        "Partagez ce jeu sur votre Facebook pour débloquer le caractère <color=green>Stela</color> gratuit.", 
        "Teilen sie dieses spiel auf ihrem Facebook, um den freien <color=green>Stela</color> charakter freizuschalten.", 
        "Comparte este juego en tu Facebook para desbloquear el personaje de <color=green>Stela</color> gratis.",
        "Compartilhe este jogo em seu Facebook para desbloquear o personagem <color=green>Stela</color> grátis.", 
        "مشاركة هذه اللعبة على الفيسبوك الخاص بك لفتح حرف <color=green>Stela</color> مجانا.", 
        "あなたの Facebook にこの ゲームを 共有して 無料の <color=green>Stela</color> キャラクターの ロックを 解除してください。",
        "무료 <color=green>Stela</color> 캐릭터의 잠금을 해제하려면 Facebook 에서이 게임을 공유하십시오.",
        "在您的 Facebook 页面上与您 的朋友分 享此游戏， 以免费解锁 <color=green>Stela</color>。", 
        "Chia sẻ trò chơi này trên Facebook của bạn để mở khóa nhân vật <color=green>Stela</color> miễn phí." 
    };
    public static List<string> menuInvite = new List<string>() 
    { 
        "INVITE", 
        "ПРИГЛАСИТЬ",
        "INVITER", 
        "EINLADEN", 
        "INVITACIÓN",
        "CONVITE", 
        "أدعو", 
        "招待", 
        "초대", 
        "邀请", 
        "MỜI BẠN BÈ" 
    };
    public static List<string> menuButtonInvite = new List<string>() 
    { 
        "INVITE NOW", 
        "ПРИГЛАСИТЬ", 
        "INVITER", 
        "EINLADEN", 
        "INVITACIÓN", 
        "CONVITE",
        "أدعو الآن", 
        "招待", 
        "초대 지금", 
        "立即邀请", 
        "MỜI NGAY" 
    };
    public static List<string> menuNoteInvite = new List<string>() 
    {
        "Send out invitations to your friends <color=green>(" + Modules.coinBonusInvite + " coins/ friend)</color>. The more friends you invite, the more coins you will get.", 
        "Отправляйте приглашения своим друзьям <color=green>(" + Modules.coinBonusInvite + " монет/ друг)</color>. Чем больше друзей пригласишь, тем больше получишь монет.", 
        "Envoyez des invitations à vos amis <color=green>(" + Modules.coinBonusInvite + " pièces/ ami)</color>. Vous invitez plus d'amis, vous aurez plus de pièces de monnaie.", 
        "Senden sie einladungen an ihre freunde <color=green>(" + Modules.coinBonusInvite + " Münzen/ Freund)</color>. Sie laden mehr freunde ein, sie werden mehr münzen.", 
        "Envía invitaciones a tus amigos <color=green>(" + Modules.coinBonusInvite + " monedas / amigo)</color>. Usted invita a más amigos, usted hará más monedas.", 
        "Envie convites para seus amigos <color=green>(" + Modules.coinBonusInvite + " moedas / amigo)</color>. Você convida mais amigos, você vai ganhar mais moedas.", 
        "إرسال دعوات إلى أصدقائك <color=green>(" + Modules.coinBonusInvite + " قطعة نقدية / صديق)</color>. يمكنك دعوة المزيد من الأصدقاء، وسوف المزيد من النقود.", 
        "お 友達 <color=green>（" + Modules.coinBonusInvite + " 枚の コイン / 友人）</color> への 招待状を 送信します。  あなたはもっと 多くの 友達を 招待します、 あなたはより 多くの コインになります。",
        "친구 <color=green>(" + Modules.coinBonusInvite + " 동전 / 친구)</color> 에게 초대장을 보냅니다. 당신은 더 많은 친구를 초대, 당신은 더 많은 동전 것입니다.", 
        "发送邀请 给你的朋友 <color=green>（" + Modules.coinBonusInvite + " 个硬币/ 朋友）</color>。 你邀请的 朋友越多， 你会得到 的钱越多。", 
        "Gửi lời mời cho bạn bè <color=green>(" + Modules.coinBonusInvite + " coins/ bạn bè)</color>. Bạn mời càng nhiều, bạn càng có nhiều coins." 
    };
    public static List<string> menuFindOpponents = new List<string>() 
    { 
        "FIND OPPONENTS", 
        "НАЙТИ ОППОНЕНТЫ",
        "TROUVER OPPONENTS", 
        "FINDEN OPPONENTEN", 
        "ENCUENTRA OPONENTES",
        "ENCONTRAR OPONENTES", 
        "البحث عن معارضون", 
        "人を 探す", 
        "상대를 찾기", 
        "寻找对手", 
        "TÌM ĐỐI THỦ" 
    };
    public static List<string> menuRate = new List<string>() 
    { 
        "REVIEW", 
        "ОЦЕНКА",
        "RÉVISER", 
        "MEINUNGEN", 
        "OPINIONES",
        "REVER",
        "استعراضات", 
        "レビュー", 
        "리뷰", 
        "回顾",
        "XẾP HẠNG" 
    };
    public static List<string> menuButtonRate = new List<string>() 
    { 
        "RATE", 
        "ОЦЕНИТЬ", 
        "RÉVISER", 
        "MEINUNGEN", 
        "OPINIONES",
        "REVER",
        "ملاحظات ", 
        "レビュー", 
        "리뷰",
        "回顾", 
        "XẾP HẠNG" 
    };
    public static List<string> menuNoteRate = new List<string>() 
    {
        "<color=green>Rate</color> and review.",
        "<color=green>Оценить</color> игру.", 
        "Aidez-nous à <color=green>améliorer</color> le jeu.", 
        "Helfen uns, das spiel zu <color=green>verbessern</color>.", 
        "Ayúdenos a <color=green>mejorar</color> el juego.", 
        "Nos ajude a <color=green>melhorar</color> o jogo.", 
        "مساعدتنا على <color=green>تحسين</color> اللعبة.", 
        "ゲームの <color=green>改善にご</color> 協力ください。",
        "우리가 게임을 <color=green>개선</color> 할 수 있도록 도와주세요.", 
        "帮助我们 <color=green>改进</color> 游戏。", 
        "<color=green>Đánh giá</color> và xếp hạng trò chơi." 
    };
    public static List<string> menuNoteHoverboard = new List<string>() 
    { 
        "Click the button below to get free <color=green>HOVERBOARD</color>.", 
        "Жми ниже, чтобы получить бесплатные <color=green>ДОСКИ</color>.", 
        "Cliquez sur le bouton ci-dessous pour obtenir <color=green>HOVERBOARD</color> gratuit.",
        "Klicken sie auf die schaltfläche unten, um kostenlos <color=green>HOVERBOARD</color> zu bekommen.",
        "Haga clic en el botón de abajo para obtener <color=green>HOVERBOARD</color> libre.", 
        "Clique no botão abaixo para obter os <color=green>HOVERBOARD</color> gratuitos.",
        "انقر فوق الزر أدناه للحصول على الحرة <color=green>HOVERBOARD</color>.", 
        "無料の <color=green>HOVERBOARD</color> を 取得するには、 下のボタンを クリックしてください。", 
        "무료 <color=green>HOVERBOARD</color> 를 받으려면 아래 버튼을 클릭하십시오.", 
        "点击下面 的按钮获 得免费的 <color=green>HOVERBOARD</color>.",
        "Ấn nút phía dưới để nhận miễn phí <color=green>VÁN BAY</color>." 
    };
    public static List<string> menuNoteKeys = new List<string>() 
    { 
        "Click the button below to get free <color=orange>KEYS</color>.", 
        "Жми, чтобы получить бесплатные <color=orange>КЛЮЧИ</color>.",
        "Cliquez sur le bouton ci-dessous pour obtenir <color=orange>CLÉS</color> gratuit.",
        "Klicken sie auf die schaltfläche unten, um freie <color=orange>SCHLÜSSEL</color> zu erhalten.", 
        "Haga clic en el botón de abajo para obtener <color=orange>LLAVES</color> gratis.", 
        "Clique no botão abaixo para obter <color=orange>CHAVES</color> livres.", 
        "انقر على الزر أدناه للحصول على <color=orange>KEYS</color> مجانية.", 
        "無料の <color=orange>KEYS</color> を 取得するには、 下のボタンを クリックしてください。", 
        "무료 <color=orange>KEYS</color> 받으려면 아래 버튼을 클릭하십시오.", 
        "点击下面 的按钮获 得免费的 <color=orange>KEYS</color>.",
        "Ấn nút phía dưới để nhận miễn phí <color=orange>CHÌA KHÓA</color>." 
    };
    public static List<string> menuPleaseWait = new List<string>() 
    { 
        "Please wait!", 
        "Подождите!", 
        "Veuillez patienter!", 
        "Bitte warten!", 
        "Espera!",
        "Esperar!", 
        "أرجو الإنتظار!", 
        "お 待ちください！", 
        "기다려주세요!", 
        "请稍候！", 
        "Vui lòng chờ!" 
    };
    public static List<string> menuClickView = new List<string>() 
    { 
        "Click view", 
        "Нажмите", 
        "Cliquez vue",
        "Klickansicht", 
        "Click ver",
        "Clique vista", 
        "عرض الآن", 
        "ビュー", 
        "보기를 클릭",
        "点击查看",
        "Kích xem" 
    };
    public static List<string> menuSetting = new List<string>() 
    { 
        "SETTINGS", 
        "НАСТРОЙКИ", 
        "RÉGLAGE", 
        "EINSTELLUNG",
        "AJUSTE",
        "CONFIGURAÇÃO", 
        "إعدادات", 
        "巣引き", 
        "놓음", 
        "设置",
        "CÀI ĐẶT" 
    };
    public static List<string> menuLanguage = new List<string>() 
    { 
        "LANGUAGE", 
        "ЯЗЫК",
        "LANGUE",
        "SPRACHE", 
        "IDIOMA",
        "LÍNGUA", 
        "لغة", 
        "言語", 
        "언어",
        "语言",
        "NGÔN NGỮ" 
    };
    public static List<string> menuBGMusic = new List<string>() 
    { 
        "BACKGROUND MUSIC", 
        "ФОНОВАЯ МУЗЫКА",
        "MUSIQUE DE FOND", 
        "HINTERGRUND MUSIK",
        "MÚSICA DE FONDO", 
        "MÚSICA DE FUNDO", 
        "خلفيه موسيقية", 
        "背景音楽", 
        "배경 음악", 
        "背景音乐", 
        "ÂM NHẠC NỀN" 
    };
    public static List<string> menuActionSound = new List<string>() 
    { 
        "GAME SOUNDS", 
        "ЗВУКИ В ИГРЕ", 
        "AUTRES MUSIQUE",
        "ANDERE MUSIK", 
        "OTRA MÚSICA", 
        "OUTRA MÚSICA",
        "موسيقى أخرى", 
        "他の 音楽", 
        "기타 음악",
        "游戏声音", 
        "ÂM THANH KHÁC" 
    };
    public static List<string> menuLevelEffect = new List<string>() 
    { 
        "LEVEL OF EFFECTS", 
        "УРОВЕНЬ ЭФФЕКТОВ", 
        "NIVEAU D'EFFETS",
        "NIVEAU EFFEKTE",
        "NIVEL DE EFECTOS",
        "NÍVEL DE EFEITOS",
        "مستوى التأثيرات", 
        "効果の レベル", 
        "효과 수준", 
        "水平的影响", 
        "MỨC ĐỘ HIỆU ỨNG" 
    };
    public static List<string> menuSkyEffect = new List<string>() 
    { 
        "SKY EFFECTS", 
        "ЭФФЕКТЫ НЕБА", 
        "EFFETS DU CIEL",
        "SKY EFFEKTE",
        "SKY EFECTOS",
        "SKY EFEITOS",
        "آثار السماء", 
        "スカイエフェクト", 
        "하늘 효과",
        "天空效应", 
        "HIỆU ỨNG BẦU TRỜI" 
    };
    public static List<string> menuSensitivity = new List<string>() 
    { 
        "SENSITIVITY", 
        "ЧУВСТВИТЕЛЬНОСТЬ", 
        "SENSIBILITÉ", 
        "EMPFINDLICHKEIT", 
        "SENSIBILIDAD", 
        "SENSIBILIDADE", 
        "حساسية", 
        "感度", 
        "감광도", 
        "灵敏度", 
        "ĐỘ NHẠY CẢM" 
    };
    public static List<string> menuSpeedJump = new List<string>() 
    { 
        "JUMP UP SPEED", 
        "СКОРОСТЬ ПРЫЖКА",
        "VITESSE DE JUMP UP",
        "JUMP GESCHWINDIGKEIT", 
        "SALTO DE VELOCIDAD",
        "SALTO DE VELOCIDADE", 
        "جومب سبيد", 
        "ジャンプアップスピード", 
        "점프 업 속도",
        "跳动速度", 
        "TỐC ĐỘ NHẢY" 
    };
    public static List<string> menuCompititor = new List<string>() 
    { 
        "COMPETITOR\n(Need to reset)", 
        "СОПЕРНИК\n(Сбросить игру)", 
        "CONCURRENT\n(Réinitialiser)",
        "KONKURRENT\n(Reset-Spiel)",
        "COMPETIDOR\n(Restablecer)", 
        "COMPETIDOR\n(Reiniciar)", 
        "كومبيتيتور\n(إعادة تعيين)",
        "競合企業\n（リセットが 必要）", 
        "경쟁사 (재설정 필요)", 
        "竞争对手\n（需要重置）",
        "ĐỐI THỦ\n(Khởi động lại)" 
    };
    public static List<string> menuCurvedWorld = new List<string>() 
    { 
        "CURVED WORLD", 
        "КРИВОЙ МИР", 
        "MONDE COURBE", 
        "GEKRÜMMTE WELT",
        "MUNDO CURVADO", 
        "MUNDO CURVADO", 
        "منحنى العالم", 
        "湾曲した 世界", 
        "곡선 세계", 
        "弯曲的世界", 
        "ĐỊA HÌNH CONG" 
    };
    public static List<string> menuPrivacyPolicy = new List<string>() 
    { 
        "PRIVACY POLICY", 
        "ПОЛИТИКА КОНФИДЕНЦИАЛЬНОСТИ", 
        "POLITIQUE PRIVÉE", 
        "DATENSCHUTZERKLÄRUNG",
        "POLÍTICA PRIVACIDAD", 
        "POLÍTICA PRIVACIDADE", 
        "سياسة الخصوصية", 
        "個人情報保護方針", 
        "개인 정보 정책", 
        "隐私政策", 
        "CHÍNH SÁCH BẢO MẬT" 
    };
    public static List<string> menuQuitGame = new List<string>() 
    { 
        "QUIT GAME", 
        "ПОКИНУТЬ ИГРУ", 
        "QUITTER LE JEU", 
        "SPIEL BEENDEN",
        "SALIR DEL JUEGO", 
        "SAIR DO JOGO", 
        "الخروج من اللعبة", 
        "出口 ゲーム", 
        "게임 나가기", 
        "退出游戏", 
        "THOÁT TRÒ CHƠI" 
    };

    //FACEBOOK SHARE
    public static List<string> menuInforDetail = new List<string>() 
    { 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.",
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " is a fun, addicting, endless running game.", 
        gameName + " rat vui. hap dan, mot the loai tro choi chay vo han." 
    };
    //I gained XXX point, in Fruntastic Squad Run Game, what about you?
    public static List<string> menuInforStart = new List<string>() 
    { 
        "I scored", 
        "I scored", 
        "I scored", 
        "I scored",
        "I scored",
        "I scored", 
        "I scored", 
        "I scored", 
        "I scored", 
        "I scored",
        "Toi dat duoc" 
    };
    public static List<string> menuInforEnd = new List<string>() 
    { 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "points, in " + gameName + ", what about you?", 
        "diem, trong tro choi " + gameName + ", con ban thi sao?" 
    };
    public static List<string> menuMessageInvite = new List<string>() 
    { 
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!",
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!", 
        "Come play this great game!" 
    };
    public static List<string> menuTitleInvite = new List<string>() 
    { 
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS",
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS", 
        "INVITE FRIENDS" 
    };

    //IN PAGE GAME PLAY
    public static List<string> playBonus = new List<string>() 
    { 
        "BONUS", 
        "БОНУС", 
        "PRIME", 
        "BONUS",
        "BONIFICACIÓN",
        "BÔNUS", 
        "علاوة",
        "ボーナス",
        "보너스",
        "奖金",
        "CON ĐƯỜNG" 
    };
    public static List<string> playRoad = new List<string>() 
    { 
        "ROAD", 
        "ДОРОГА",
        "ROUTE",
        "STRASSE",
        "CAMINO",
        "ESTRADA",
        "طريق",
        "道路", 
        "도로",
        "道路",
        "PHẦN THƯỞNG" 
    };
    public static List<string> playWinTop = new List<string>() 
    { 
        "YOU", 
        "ВЫ", 
        "TOI", 
        "SIE",
        "TÚ",
        "VOCÊ", 
        "أنت",
        "君が",
        "당신",
        "您", 
        "CHIẾN" 
    };
    public static List<string> playLoseTop = new List<string>() 
    { 
        "YOU", 
        "ВЫ", 
        "TOI", 
        "SIE",
        "TÚ",
        "VOCÊ", 
        "أنت",
        "君が",
        "당신",
        "您",
        "THẤT" 
    };
    public static List<string> playWinDown = new List<string>() 
    { 
        "WIN", 
        "ВЫИГРАТЬ",
        "GAGNER",
        "SIEG",
        "GANAR",
        "GANHAR",
        "يفوز",
        "勝つ", 
        "승리",
        "赢得",
        "THẮNG" 
    };
    public static List<string> playLoseDown = new List<string>() 
    { 
        "LOSE", 
        "ПОТЕРЯТЬ",
        "PERDRE",
        "VERLIEREN",
        "PERDER",
        "PERDER",
        "تخسر",
        "失う", 
        "잃다",
        "失去",
        "BẠI" 
    };
    public static List<string> playYouWin = new List<string>() 
    { 
        "YOU WIN", 
        "ПОБЕДА",
        "VOUS GAGNEZ",
        "DU GEWINNST",
        "TÚ GANAS",
        "VOCÊ GANHA",
        "أنت فوز",
        "あなたは 勝ちます", 
        "네가 이겼다",
        "你赢了",
        "CHIẾN THẮNG" 
    };
    public static List<string> playYouLose = new List<string>() 
    { 
        "YOU LOSE", 
        "ПОРАЖЕНИЕ",
        "VOUS PERDEZ",
        "DU VERLIERST",
        "TÚ PIERDES",
        "VOCÊ PERDEU",
        "تفقد",
        "あなたは 失う", 
        "너 잃어",
        "你输了",
        "THẤT BẠI" 
    };
    public static List<string> playCoins = new List<string>() 
    { 
        "coins", 
        "монеты",
        "pièces",
        "münzen",
        "monedas",
        "moedas",
        "صار",
        "コイン", 
        "동전",
        "硬币",
        "đồng xu" 
    };
    public static List<string> playWinXTimes = new List<string>() 
    { 
        "WIN ? TIMES", 
        "ВЫИГРАТЬ ? РАЗ",
        "GAGNER ? FOIS",
        "GEWINNE ? MAL",
        "GANA ? VECES",
        "GANHE ? VEZES",
        "الفوز ? مرات",
        "? 回勝つ", 
        "? 번 우승",
        "赢 ? 次", 
        "THẮNG ? LẦN" 
    };
    public static List<string> playBonusTitle = new List<string>() 
    { 
        "BONUS", 
        "БОНУС",
        "PRIME", 
        "BONUS", 
        "PRIMA",
        "BÔNUS", 
        "علاوة", 
        "ボーナス", 
        "보너스", 
        "奖金", 
        "THƯỞNG" 
    };
    public static List<string> playBonusButton = new List<string>() 
    { 
        "OKAY", 
        "ЛАДНО", 
        "OKAY", 
        "OKAY", 
        "OKAY", 
        "OKAY",
        "حسنا", 
        "はい", 
        "괜찮아", 
        "好的",
        "ĐƯỢC" 
    };
    public static List<string> playBonusContent = new List<string>() 
    {
        "You get <color=orange>5000</color> coins and unlock the <color=green>Outspace SL</color> for the first time play.", 
        "Вы получаете <color=orange>5000</color> монет и разблокируете <color=green>Outspace SL</color> в первый раз.", 
        "Vous obtenez <color=orange>5000</color> pièces et débloquez l'<color=green>Outspace SL</color> pour la première fois.",
        "Sie erhalten <color=orange>5000</color> münzen und entsperren sie das <color=green>Outspace SL</color> zum ersten mal spielen.",
        "Usted consigue <color=orange>5000</color> monedas y desbloquear el <color=green>Outspace SL</color> por primera vez jugar.",
        "Você ganha <color=orange>5000</color> moedas e desbloqueia o <color=green>Outspace SL</color> pela primeira vez joga.",
        "يمكنك الحصول على <color=orange>5000</color> النقود وفتح <color=green>Outspace SL</color> لأول مرة اللعب.",
        "あなたは <color=orange>5000</color> コインを 獲得し、 初めて プレイするために <color=green>Outspace SL</color> の ロックを 解除します。",
        "<color=orange>5000</color> 동전을 얻고 <color=green>Outspace SL</color> 를 처음으로 연다.",
        "你会获得 <color=orange>5000</color> 个硬币，并首次 打开 <color=green>Outspace SL</color> 打。", 
        "Bạn nhận được <color=orange>5000</color> đồng tiền và mở khóa <color=green>Outspace SL</color> cho lần chơi đầu tiên."
    };
    public static List<string> playHighScore = new List<string>() 
    { 
        "HIGH SCORE", 
        "БОЛЬШОЙ СЧЁТ",
        "SCORE ÉLEVÉ",
        "HIGH SCORE", 
        "ALTA PUNTUACIÓN", 
        "ALTA PONTUAÇÃO",
        "أعلى الدرجات", 
        "高得点", 
        "높은 점수", 
        "高分", 
        "ĐIỂM KỶ LỤC" 
    };

    //IN GUIDE
    public static List<string> playSwipeLeft = new List<string>() 
    { 
        "Swipe left", 
        "Проведи в лево", 
        "Swipe gauche", 
        "Wischen links", 
        "Deslizar izquierda",
        "Deslizar esquerda",
        "اسحب يسار", 
        "左 スワイプ", 
        "슬쩍 왼쪽",
        "向左滑动", 
        "Vuốt trái" 
    };
    public static List<string> playSwipeRight = new List<string>() 
    { 
        "Swipe right", 
        "Проведи в право", 
        "Swipe droite",
        "Wischen richtig",
        "Deslizar derecha",
        "Deslizar direita",
        "اسحب يمين", 
        "右 スワイプ", 
        "슬쩍 오른쪽",
        "向右滑动", 
        "Vuốt phải" 
    };
    public static List<string> playSwipeUp = new List<string>() 
    { 
        "Swipe up", 
        "Проведи вверх", 
        "Swipe haut", 
        "Wischen oben",
        "Deslizar arriba",
        "Deslizar cima",
        "اسحب للاعلى", 
        "上 スワイプ",
        "슬쩍 위",
        "向上滑动", 
        "Vuốt lên" 
    };
    public static List<string> playSwipeDown = new List<string>() 
    { 
        "Swipe down", 
        "Проведи вниз", 
        "Swipe bas",
        "Wischen unten",
        "Deslizar abajo",
        "Deslizar baixo",
        "اسحب أسفل", 
        "下 スワイプ",
        "슬쩍 아래",
        "向下滑动",
        "Vuốt xuống" 
    };
    public static List<string> playUseHoverboard = new List<string>() 
    { 
        "Double tap to\nuse <color=lime>HOVERBOARD</color>", 
        "Нажми дважды, чтобы\nиспользовать <color=lime>ДОСКУ</color>", 
        "Appuyez deux fois\nà utiliser <color=lime>HOVERBOARD</color>", 
        "Tippen sie zweimal\nverwenden <color=lime>HOVERBOARD</color>", 
        "Doble toque para\nusar <color=lime>HOVERBOARD</color>", 
        "Toque duas vezes para\nusar <color=lime>HOVERBOARD</color>", 
        "انقر نقرا مزدوجا\nلاستخدام <color=lime>HOVERBOARD</color>",
        "<color=lime>HOVERBOARD</color> を 使用するに\nは ダブルタップしてください", 
        "<color=lime>HOVERBOARD</color> 를 사용하려면 두 번 탭하세요.", 
        "双击使用 <color=lime>HOVERBOARD</color>。", 
        "Ấn đúp để\nsử dụng <color=lime>VÁN BAY</color>" 
    };
    public static List<string> playUseHoverbike = new List<string>() 
    { 
        "To use <color=lime>HOVERBIKE</color>\nTap the button with the icon", 
        "Чтобы использовать <color=lime>ЛЕТОЦИКЛ</color>\nНажми кнопку со значком", 
        "Pour utiliser <color=lime>HOVERBIKE</color>\nAppuyez sur le bouton avec l'icône", 
        "So verwenden sie <color=lime>HOVERBIKE</color>\nTippen sie auf die schaltfläche mit dem symbol", 
        "Para usar <color=lime>HOVERBIKE</color>\nToque el botón con el icono", 
        "Para usar <color=lime>HOVERBIKE</color>\nToque no botão com o ícone", 
        "لاستخدام <color=lime>HOVERBIKE</color>\nاضغط على زر مع رمز", 
        "<color=lime>HOVERBIKE</color> を 使用するには\nアイコン 付きの ボタンを タップします", 
        "<color=lime>HOVERBIKE</color> 를 사용하려면\n아이콘이있는 버튼을 누릅니다.", 
        "使用 <color=lime>HOVERBIKE</color>\n用图标点 击按钮", 
        "Để sử dụng <color=lime>XE MÁY BAY</color>\nẤn nút có biểu tượng sau"
    };
    public static List<string> playBeginMove = new List<string>() 
    { 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Классные движения!\n<color=lime>Погнали!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>",
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Gorgeous moves!\n<color=lime>Let's GO!</color>", 
        "Cuộc hành trình\n<color=lime>Bắt đầu!</color>"
    };

    //OTHER MESSAGES
    public static List<string> playSaveMe = new List<string>() 
    { 
        "Save me!", 
        "Спаси меня!",
        "Sauve-moi!", 
        "Rette mich!",
        "Salvame!",
        "Salve-me!",
        "انقذني!", 
        "私を 救う！",
        "구해줘!", 
        "救我！", 
        "Cứu tôi!" 
    };
    public static List<string> playSetting = new List<string>() 
    { 
        "SETTINGS", 
        "НАСТРОЙКИ", 
        "RÉGLAGE", 
        "EINSTELLUNG",
        "AJUSTE",
        "CONFIGURAÇÃO", 
        "إعدادات", 
        "巣引き",
        "놓음", 
        "设置", 
        "CÀI ĐẶT" 
    };
    public static List<string> playGoHome = new List<string>() 
    { 
        "HOME", 
        "ГЛАВНАЯ",
        "DOMICILE",
        "HEIMFAHREN",
        "IR CASA",
        "IR CASA", 
        "للمنزل", 
        "ホーム",
        "집에가",
        "回家", 
        "QUAY VỀ" 
    };
    public static List<string> playStartIn = new List<string>() 
    { 
        "Resume in", 
        "Продолжить через", 
        "Commence en", 
        "Startet in",
        "Empezar en",
        "Comece em", 
        "يبدأ في", 
        "始める",
        "에서 시작", 
        "简历中", 
        "Tiếp tục trong" 
    };
    public static List<string> playKeyNeed = new List<string>() 
    { 
        "NEED KEYS:", 
        "НУЖНЫ КЛЮЧИ:",
        "BESOIN CLÉS:",
        "BRAUCHEN:", 
        "NECESITAR:", 
        "NECESSIDADE:",
        "مفاتيح تحتاج:", 
        "ニーズ：",
        "요하다:",
        "需要关键：",
        "CHÌA KHÓA CẦN:" 
    };

    //IN PAGE HERO/HOVERBOARD
    public static List<string> heroCharacters = new List<string>() 
    { 
        "CHARACTERS",
        "ПЕРСОНАЖИ", 
        "PERSONNAGES",
        "CHARAKTERE",
        "CARACTERES",
        "PERSONAGENS",
        "الشخصيات",
        "キャラクター",
        "등장인물",
        "人物",
        "NHÂN VẬT" 
    };
    public static List<string> heroHoverboard = new List<string>() 
    { 
        "HOVERBOARD", 
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD",
        "VÁN BAY" 
    };
    public static List<string> heroNotEnough = new List<string>() 
    { 
        "NOT ENOUGH", 
        "НЕ ДОСТАТОЧНО",
        "PAS ASSEZ",
        "NICHT GENUG",
        "NO BASTA",
        "NÃO BASTANTE", 
        "ليس كافي", 
        "十分ではない",
        "충분하지 않은",
        "不够",
        "KHÔNG ĐỦ" 
    };
    public static List<string> heroSelected = new List<string>() 
    { 
        "SELECTED", 
        "ВЫБРАНО", 
        "CHOISI", 
        "AUSGEWÄHLT",
        "ESCOGIDO", 
        "SELECIONADO", 
        "مختارة",
        "選択された",
        "선택된",
        "选定",
        "ĐÃ CHỌN" 
    };
    public static List<string> heroUnlocked = new List<string>() 
    { 
        "UNLOCKED", 
        "РАЗБЛОКИРОВАН", 
        "OUVRIR",
        "ÖFFNEN",
        "ABIERTO",
        "ABERTO", 
        "مفتوحة",
        "アンロック",
        "잠금 해제 됨", 
        "未锁定", 
        "ĐÃ MUA" 
    };
    public static List<List<string>> heroInfoHero = new List<List<string>>() { 
        new List<string>() 
        {
            "Name: Max\nMale\nEuropian\n14 years old\nHeight: 145 cm\nWeight: 45 kg\nHobbies: Parkour",
            "Имя: Max\nМужской\nЕвропейская\n14 лет\nРост: 145 cm\nВес: 45 kg\nУвлечения: Parkour",
            "Nom: Max\nHomme\nEuropian\n14 ans\nTaille: 145 cm\nPoids: 45 kg\nLoisirs: Parkour",
            "Name: Max\nMännlich\nEuropäisch\n14 Jahre alt\nGröße: 145 cm\nGewicht: 45 kg\nHobbys: Parkour",
            "Nombre: Max\nHombre\nEuropian\n14 años\nAltura: 145 cm\nPeso: 45 kg\nPasatiempos: Parkour",
            "Nome: Max\nMasculino\nEuropian\n14 anos\nAltura: 145 cm\nPeso: 45 kg\nPassatempos: Parkour",
            "الاسم: Max\nذكر \nأوروبي \n14 سنة \nالطول: 145 سم \nالوزن: 45 كجم \nالهوايات: باركور",
            "名前：Max\n男性\nヨーロッパ\n14 歳\n身長：145 cm\n体重：45 kg\n趣味： パルクール",
            "이름 : Max\n남성\n유럽\n14 세\n높이 : 145 cm\n없음 체중 : 45 kg\n취미 : 파 쿠르",
            "姓名：Max\n男性\n欧洲人\n14岁\n身高：145 厘米\n体重：45 公斤\n爱好： 跑酷",
            "Tên: Max\nNam giới\nChâu Âu\n14 tuổi\nCao: 145 cm\nNặng: 45 kg\nSở thích: Thể thao"
        }, 
        new List<string>() 
        {
            "Name: Stella\nFemale\nHispanic\n13 years old\nHeight: 140 cm\nWeight: 40 kg\nHobbies: Dancing",
            "Имя: Stella\nЖенщина\nЛатиноамериканец\n13 лет\nРост: 140 cm\nВес: 40 kg\nУвлечения: Танцы",
            "Nom: Stella\nFemme\nHispanique\n13 ans\nTaille: 140 cm\nPoids: 40 kg\nLoisirs: Danse",
            "Name: Stella\nWeiblich\nHispanic\n13 Jahre alt\nGröße: 140 cm\nGewicht: 40 kg\nHobby: Tanzen",
            "Nombre: Stella\nHembra\nHispana\n13 años\nAltura: 140 cm\nPeso: 40 kg\nPasatiempos: Bailar",
            "Nome: Stella\nFeminino\nHispânico\n13 anos\nAltura: 140 cm\nPeso: 40 kg\nPassatempos: Dança",
            "الاسم: Stella\nأنثى \nاسباني \n13 سنة \nالطول: 140 سم \nالوزن: 40 كجم \nالهوايات: الرقص",
            "名前：Stella\n女性\nヒスパニック\n13 歳\n身長：140 cm\n体重：40 kg\n趣味： ダンス",
            "이름 : Stella\n여성\n히스패닉\n13 세\n높이 : 140 cm\n무게 : 40 kg\n취미 : 춤",
            "姓名：Stella\n女性\n西班牙裔\n13岁\n身高：140 厘米\n体重：40 公斤\n爱好： 跳舞",
            "Tên: Stella\nNữ giới\nTây Ban Nha\n13 tuổi\nCao: 140 cm\nNặng: 40 kg\nSở thích: Khiêu vũ"
        },
        new List<string>() 
        {
            "Name: Tadita\nFemale\nNative American\n13 years old\nHeight: 148 cm\nWeight: 43 kg\nHobbies: Running",
            "Имя: Tadita\nЖенщина\nРодной американец\n13 лет\nРост: 148 cm\nВес: 43 kg\nУвлечения: Бег",
            "Nom: Tadita\nFemme\nAmérindien\n13 ans\nTaille: 148 cm\nPoids: 43 kg\nLoisirs: Course",
            "Name: Tadita\nWeiblich\nNative American\n13 Jahre alt\nGröße: 148 cm\nGewicht: 43 kg\nHobby: Laufen",
            "Nombre: Tadita\nFemenino\nNativo Americano\n13 años\nAltura: 148 cm\nPeso: 43 kg\nPasatiempos: Running",
            "Nome: Tadita\nFeminino\nNativo Americano\n13 anos\nAltura: 148 cm\nPeso: 43 kg\nPassatempos: Correndo",
            "الاسم: Tadita\nأنثى \nالأمريكيين \n13 سنة \nالطول: 148 سم \nالوزن: 43 كجم \nالهوايات: الجري",
            "名前：Tadita\n女性\nアメリカ先住民\n13 歳\n身長：148 cm\n体重：43 kg\n趣味： ランニング",
            "이름 : Tadita\n여성\n아메리카 인디언\n13 세\n높이 : 148 cm\n무게 : 43 kg\n취미 : 러닝",
            "姓名：Tadita\n女性\n美洲原住民\n13岁\n身高：148 厘米\n体重：43 公斤\n爱好： 跑步",
            "Tên: Tadita\nNữ giới\nNgười Mỹ\n13 tuổi\nCao: 148 cm\nNặng: 43 kg\nSở thích: Chạy nhảy"
        },
        new List<string>() 
        {
            "Name: Akio\nMale\nJapanese\n14 years old\nHeight: 138 cm\nWeight: 38 kg\nHobbies: Martial Arts",
            "Имя: Akio\nМужской\nЯпонский\n14 лет\nРост: 138 cm\nВес: 38 kg\nУвлечения: Боевые искусства",
            "Nom: Akio\nHomme\nJaponais\n14 ans\nTaille: 138 cm\nPoids: 38 kg\nLoisirs: Arts martiaux",
            "Name: Akio\nMännlich\nJapanisch\n14 Jahre alt\nGröße: 138 cm\nGewicht: 38 kg\nHobby: Martial Arts",
            "Nombre: Akio\nHombre\nJaponés\n14 años\nAltura: 138 cm\nPeso: 38 kg\nPasatiempos: Artes Marciales",
            "Nome: Akio\nMale\nJapanese\n14 years\nAltura: 138 cm\nPeso: 38 kg\nPassatempos: Artes Marciais",
            "الاسم: Akio\nذكر \nياباني \n14 سنة \nالطول: 138 سم \nالوزن: 38 كجم \nالهوايات: فنون الدفاع عن النفس",
            "名前：Akio\n男性\n日本人\n14 歳\n身長：138 cm\n体重：38 kg\n趣味： 格闘技",
            "이름 : Akio\n남성\n일본\n14 세\n높이 : 138 cm\n무게 : 38 kg\n취미 : 격투기",
            "姓名：Akio\n男\n日语\n14岁\n身高：138 厘米\n体重：38 公斤\n爱好： 武术",
            "Tên: Akio\nNam giới\nNhật Bản\n14 tuổi\nCao: 138 cm\nNặng: 38 kg\nSở thích: Võ thuật"
        },
        new List<string>() 
        {
            "Name: Runubis\nOrigin Egypt\nHeight: 150 cm\nWeight: 48 kg\nHobbies: Mystical Adventures",
            "Имя: Runubis\nПроисхождение Египет\nВысота: 150 cm\nВес: 48 kg\nУвлечения: Мистические приключения",
            "Nom: Runubis\nOrigine Egypte\nTaille: 150 cm\nPoids: 48 kg\nLoisirs: aventures mystiques",
            "Name: Runubis\nHerkunft Ägypten\nGröße: 150 cm\nGewicht: 48 kg\nHobby: Mystical Adventures",
            "Nombre: Runubis\nOrigen Egipto\nAltura: 150 cm\nPeso: 48 kg\nPasatiempos: Mystical Adventures",
            "Nome: Runubis\nOrigem Egito\nAltura: 150 cm\nPeso: 48 kg\nPasatiempos: Mystical Adventures",
            "الاسم: Runubis\nالأصل مصر \nالطول: 150 سم \nالوزن: 48 كجم \nالهوايات: مغامرات باطني",
            "名前：Runubis\n原産地エジプト\n身長：150 cm\n体重：48 kg\n趣味： 神秘的な冒険",
            "이름 : Runubis\n원산지 이집트\n높이 : 150 cm\n무게 : 48 kg\n취미 : 신비로운 모험",
            "姓名：Runubis\n产地埃及\n身高：150 厘米\n体重：48 公斤\n爱好： 神秘冒险",
            "Tên: Runubis\nAi Cập Cổ\nCao: 150 cm\nNặng: 48 kg\nSở thích: Phép thuật"
        },
        new List<string>() 
        {
            "Name: Rock Star\nAmerican\nHeight: 140 cm\nWeight: 40 kg\nHobbies: Rock music",
            "Имя: Rock Star\nАмериканская\nВысота: 140 cm\nВес: 40 kg\nУвлечения: Рок музыка",
            "Nom: Rock Star\nAméricain\nTaille: 140 cm\nPoids: 40 kg\nLoisirs: Musique rock",
            "Name: Rock Star\nAmerikanisch\nGröße: 140 cm\nGewicht: 40 kg\nHobby: Rockmusik",
            "Nombre: Rock Star\nAmericano\nAltura: 140 cm\nPeso: 40 kg\nPasatiempos: Música rock",
            "Nome: Rock Star\nAmericano\nAltura: 140 cm\nPeso: 40 kg\nPasatiempos: Musica rock",
            "الاسم: Rock Star\nأمريكي \nالطول: 140 سم \nالوزن: 40 كجم \nموسيقى الروك",
            "名前：Rock Star\nアメリカ人\n身長：140 cm\n体重：40 kg\n趣味： ロックミュージック",
            "이름 : Rock Star\n미국 사람\n높이 : 140 cm\n무게 : 40 kg\n취미 : 록 음악",
            "姓名：Rock Star\n美国\n身高：140 厘米\n体重：40 公斤\n爱好： 摇滚音乐",
            "Tên: Rock Star\nNgười Mỹ\nCao: 140 cm\nNặng: 40 kg\nSở thích: Nhạc Rock"
        },
        new List<string>() 
        {
            "Name: Kiki\nAmerican\nHeight: 145 cm\nWeight: 42 kg\nHobbies: Loves to Party",
            "Имя: Kiki\nАмериканская\nВысота: 145 cm\nВес: 42 kg\nУвлечения: Любит вечеринки",
            "Nom: Kiki\nAméricain\nTaille: 145 cm\nPoids: 42 kg\nLoisirs: Aime faire la fête",
            "Name: Kiki\nAmerikanisch\nGröße: 145 cm\nGewicht: 42 kg\nHobby: Liebt zur Party",
            "Nombre: Kiki\nAmericano\nAltura: 145 cm\nPeso: 42 kg\nPasatiempos: Ama a la fiesta",
            "Nome: Kiki\nAmericano\nAltura: 145 cm\nPeso: 42 kg\nPasatiempos: Gosta de festa",
            "الاسم: Kiki\nأمريكي \nالطول: 145 سم \nالوزن: 42 كجم \nيحب الحزب",
            "名前：Kiki\nアメリカ人\n身長：145 cm\n体重：42 kg\n趣味： パーティーを 愛する",
            "이름 : Kiki\n미국 사람\n높이 : 145 cm\n무게 : 42 kg\n취미 : 파티를 사랑한다",
            "姓名：Kiki\n美国\n身高：145 厘米\n体重：42 公斤\n爱好： 喜欢派对",
            "Tên: Kiki\nNgười Mỹ\nCao: 145 cm\nNặng: 42 kg\nSở thích: Thích Tiệc"
        }
    };
    public static List<List<string>> heroInfoHoverboard = new List<List<string>>() { 
        new List<string>() 
        {
            "Tourister 2000\nLength: 75 cm\nWidth: 24 cm\nWeight: 2.0 kg",
            "Tourister 2000\nДлина: 75 cm\nШирина: 24 cm\nВес: 2.0 kg",
            "Tourister 2000\nLongueur: 75 cm\nLargeur: 24 cm\nPoids: 2.0 kg",
            "Tourister 2000\nLänge: 75 cm\nBreite: 24 cm\nGewicht: 2.0 kg",
            "Tourister 2000\nLargo: 75 cm\nAncho: 24 cm\nPeso: 2.0 kg",
            "Tourister 2000\nComprimento: 75 cm\nLargura: 24 cm\nPeso: 2.0 kg",
            "Tourister 2000\nالطول: 75 سم \nالعرض: 24 سم \nالوزن: 2.0 كجم",
            "Tourister 2000\n長さ：75 cm\n幅：24 cm\n重量：2.0 kg",
            "Tourister 2000\n길이: 75 cm\n너비: 24 cm\n무게: 2.0 kg",
            "Tourister 2000\n长度：75 厘米\n宽度：24 厘米\n重量：2.0 公斤",
            "Tourister 2000\nDài: 75 cm\nRộng: 24 cm\nNặng: 2.0 kg"
        }, 
        new List<string>() 
        {
            "Outspace SL\nLength: 70 cm\nWidth: 22 cm\nWeight: 2.5 kg",
            "Outspace SL\nДлина: 70 cm\nШирина: 22 cm\nВес: 2.5 kg",
            "Outspace SL\nLongueur: 70 cm\nLargeur: 22 cm\nPoids: 2.5 kg",
            "Outspace SL\nLänge: 70 cm\nBreite: 22 cm\nGewicht: 2.5 kg",
            "Outspace SL\nLargo: 70 cm\nAncho: 22 cm\nPeso: 2.5 kg",
            "Outspace SL\nComprimento: 70 cm\nLargura: 22 cm\nPeso: 2.5 kg",
            "Outspace SL\nالطول: 70 سم \nالعرض: 22 سم \nالوزن: 2.5 كجم",
            "Outspace SL\n長さ：70 cm\n幅：22 cm\n重量：2.5 kg",
            "Outspace SL\n길이: 70 cm\n너비: 22 cm\n무게: 2.5 kg",
            "Outspace SL\n长度：70 厘米\n宽度：22 厘米\n重量：2.5 公斤",
            "Outspace SL\nDài: 70 cm\nRộng: 22 cm\nNặng: 2.5 kg"
        }, 
        new List<string>() 
        {
            "Splitex G Force\nLength: 65 cm\nWidth: 20 cm\nWeight: 1.5 kg",
            "Splitex G Force\nДлина: 65 cm\nШирина: 20 cm\nВес: 1.5 kg",
            "Splitex G Force\nLongueur: 65 cm\nLargeur: 20 cm\nPoids: 1.5 kg",
            "Splitex G Force\nLänge: 65 cm\nBreite: 20 cm\nGewicht: 1.5 kg",
            "Splitex G Force\nLargo: 65 cm\nAncho: 20 cm\nPeso: 1.5 kg",
            "Splitex G Force\nComprimento: 65 cm\nLargura: 20 cm\nPeso: 1.5 kg",
            "Splitex G Force\nالطول: 65 سم \nالعرض: 20 سم \nالوزن: 1.5 كجم",
            "Splitex G Force\n長さ：65 cm\n幅：20 cm\n重量：1.5 kg",
            "Splitex G Force\n길이: 65 cm\n너비: 20 cm\n무게: 1.5 kg",
            "Splitex G Force\n长度：65 厘米\n宽度：20 厘米\n重量：1.5 公斤",
            "Splitex G Force\nDài: 65 cm\nRộng: 20 cm\nNặng: 1.5 kg"
        }, 
        new List<string>() 
        {
            "Speedster\nLength: 80 cm\nWidth: 26 cm\nWeight: 2.2 kg",
            "Speedster\nДлина: 80 cm\nШирина: 26 cm\nВес: 2.2 kg",
            "Speedster\nLongueur: 80 cm\nLargeur: 26 cm\nPoids: 2.2 kg",
            "Speedster\nLänge: 80 cm\nBreite: 26 cm\nGewicht: 2.2 kg",
            "Speedster\nLargo: 80 cm\nAncho: 26 cm\nPeso: 2.2 kg",
            "Speedster\nComprimento: 80 cm\nLargura: 26 cm\nPeso: 2.2 kg",
            "Speedster\nالطول: 80 سم \nالعرض: 26 سم \nالوزن: 2.2 كجم",
            "Speedster\n長さ：80 cm\n幅：26 cm\n重量：2.2 kg",
            "Speedster\n길이: 80 cm\n너비: 26 cm\n무게: 2.2 kg",
            "Speedster\n长度：80 厘米\n宽度：26 厘米\n重量：2.2 公斤",
            "Speedster\nDài: 80 cm\nRộng: 26 cm\nNặng: 2.2 kg"
        }, 
        new List<string>() 
        {
            "Impedus Magnus\nLength: 82 cm\nWidth: 30 cm\nWeight: 2.5 kg",
            "Impedus Magnus\nДлина: 82 cm\nШирина: 30 cm\nВес: 2.5 kg",
            "Impedus Magnus\nLongueur: 82 cm\nLargeur: 30 cm\nPoids: 2.5 kg",
            "Impedus Magnus\nLänge: 82 cm\nBreite: 30 cm\nGewicht: 2.5 kg",
            "Impedus Magnus\nLargo: 82 cm\nAncho: 30 cm\nPeso: 2.5 kg",
            "Impedus Magnus\nComprimento: 82 cm\nLargura: 30 cm\nPeso: 2.5 kg",
            "Impedus Magnus\nالطول: 82 سم \nالعرض: 30 سم \nالوزن: 2.5 كجم",
            "Impedus Magnus\n長さ：82 cm\n幅：30 cm\n重量：2.5 kg",
            "Impedus Magnus\n길이: 82 cm\n너비: 30 cm\n무게: 2.5 kg",
            "Impedus Magnus\n长度：82 厘米\n宽度：30 厘米\n重量：2.5 公斤",
            "Impedus Magnus\nDài: 82 cm\nRộng: 30 cm\nNặng: 2.5 kg"
        },
        new List<string>() 
        {
            "Hunny Bunny\nLength: 78 cm\nWidth: 25 cm\nWeight: 1.8 kg",
            "Hunny Bunny\nДлина: 78 cm\nШирина: 25 cm\nВес: 1.8 kg",
            "Hunny Bunny\nLongueur: 78 cm\nLargeur: 25 cm\nPoids: 1.8 kg",
            "Hunny Bunny\nLänge: 78 cm\nBreite: 25 cm\nGewicht: 1.8 kg",
            "Hunny Bunny\nLargo: 78 cm\nAncho: 25 cm\nPeso: 1.8 kg",
            "Hunny Bunny\nComprimento: 78 cm\nLargura: 25 cm\nPeso: 1.8 kg",
            "Hunny Bunny\nالطول: 78 سم \nالعرض: 25 سم \nالوزن: 1.8 كجم",
            "Hunny Bunny\n長さ：78 cm\n幅：25 cm\n重量：1.8 kg",
            "Hunny Bunny\n길이: 78 cm\n너비: 25 cm\n무게: 1.8 kg",
            "Hunny Bunny\n长度：78 厘米\n宽度：25 厘米\n重量：1.8 公斤",
            "Hunny Bunny\nDài: 78 cm\nRộng: 25 cm\nNặng: 1.8 kg"
        },
        new List<string>() 
        {
            "Rock Guitar\nLength: 88 cm\nWidth: 23 cm\nWeight: 2.1 kg",
            "Rock Guitar\nДлина: 88 cm\nШирина: 23 cm\nВес: 2.1 kg",
            "Rock Guitar\nLongueur: 88 cm\nLargeur: 23 cm\nPoids: 2.1 kg",
            "Rock Guitar\nLänge: 88 cm\nBreite: 23 cm\nGewicht: 2.1 kg",
            "Rock Guitar\nLargo: 88 cm\nAncho: 23 cm\nPeso: 2.1 kg",
            "Rock Guitar\nComprimento: 88 cm\nLargura: 23 cm\nPeso: 2.1 kg",
            "Rock Guitar\nالطول: 88 سم \nالعرض: 23 سم \nالوزن: 2.1 كجم",
            "Rock Guitar\n長さ：88 cm\n幅：23 cm\n重量：2.1 kg",
            "Rock Guitar\n길이: 88 cm\n너비: 23 cm\n무게: 2.1 kg",
            "Rock Guitar\n长度：88 厘米\n宽度：23 厘米\n重量：2.1 公斤",
            "Rock Guitar\nDài: 88 cm\nRộng: 23 cm\nNặng: 2.1 kg"
        },
        new List<string>() 
        {
            "Gold Wings of Horus\nLength: 80 cm\nWidth: 28 cm\nWeight: 0.5 kg",
            "Gold Wings of Horus\nДлина: 80 cm\nШирина: 28 cm\nВес: 0.5 kg",
            "Gold Wings of Horus\nLongueur: 80 cm\nLargeur: 28 cm\nPoids: 0.5 kg",
            "Gold Wings of Horus\nLänge: 80 cm\nBreite: 28 cm\nGewicht: 0.5 kg",
            "Gold Wings of Horus\nLargo: 80 cm\nAncho: 28 cm\nPeso: 0.5 kg",
            "Gold Wings of Horus\nComprimento: 80 cm\nLargura: 28 cm\nPeso: 0.5 kg",
            "Gold Wings of Horus\nالطول: 80 سم \nالعرض: 28 سم \nالوزن: 0.5 كجم",
            "Gold Wings of Horus\n長さ：80 cm\n幅：28 cm\n重量：0.5 kg",
            "Gold Wings of Horus\n길이: 80 cm\n너비: 28 cm\n무게: 0.5 kg",
            "Gold Wings of Horus\n长度：80 厘米\n宽度：28 厘米\n重量：0.5 公斤",
            "Gold Wings of Horus\nDài: 80 cm\nRộng: 28 cm\nNặng: 0.5 kg"
        },
        new List<string>() 
        {
            "Kareta\nLength: 75 cm\nWidth: 40 cm\nWeight: 3.5 kg",
            "Kareta\nДлина: 75 cm\nШирина: 40 cm\nВес: 3.5 kg",
            "Kareta\nLongueur: 75 cm\nLargeur: 40 cm\nPoids: 3.5 kg",
            "Kareta\nLänge: 75 cm\nBreite: 40 cm\nGewicht: 3.5 kg",
            "Kareta\nLargo: 75 cm\nAncho: 40 cm\nPeso: 3.5 kg",
            "Kareta\nComprimento: 75 cm\nLargura: 40 cm\nPeso: 3.5 kg",
            "Kareta\nالطول: 75 سم \nالعرض: 40 سم \nالوزن: 3.5 كجم",
            "Kareta\n長さ：75 cm\n幅：40 cm\n重量：3.5 kg",
            "Kareta\n길이: 75 cm\n너비: 40 cm\n무게: 3.5 kg",
            "Kareta\n长度：75 厘米\n宽度：40 厘米\n重量：3.5 公斤",
            "Kareta\nDài: 75 cm\nRộng: 40 cm\nNặng: 3.5 kg"
        },
        new List<string>() 
        {
            "Turbo GX Sport\nLength: 80 cm\nWidth: 50 cm\nWeight: 3.8 kg",
            "Turbo GX Sport\nДлина: 80 cm\nШирина: 50 cm\nВес: 3.8 kg",
            "Turbo GX Sport\nLongueur: 80 cm\nLargeur: 50 cm\nPoids: 3.8 kg",
            "Turbo GX Sport\nLänge: 80 cm\nBreite: 50 cm\nGewicht: 3.8 kg",
            "Turbo GX Sport\nLargo: 80 cm\nAncho: 50 cm\nPeso: 3.8 kg",
            "Turbo GX Sport\nComprimento: 80 cm\nLargura: 50 cm\nPeso: 3.8 kg",
            "Turbo GX Sport\nالطول: 80 سم \nالعرض: 50 سم \nالوزن: 3.8 كجم",
            "Turbo GX Sport\n長さ：80 cm\n幅：50 cm\n重量：3.8 kg",
            "Turbo GX Sport\n길이: 80 cm\n너비: 50 cm\n무게: 3.8 kg",
            "Turbo GX Sport\n长度：80 厘米\n宽度：50 厘米\n重量：3.8 公斤",
            "Turbo GX Sport\nDài: 80 cm\nRộng: 50 cm\nNặng: 3.8 kg"
        }
    };

    //IN PAGE TOP/ACHIEVEMENT
    public static List<string> topScore = new List<string>() 
    { 
        "SCORE", 
        "СЧЕТ",
        "SCORE", 
        "SCORE",
        "SCORE", 
        "PONTO",
        "أحرز هدفاً", 
        "スコア",
        "점수",
        "得分了",
        "ĐIỂM" 
    };
    public static List<string> topNoName = new List<string>() 
    { 
        "No name", 
        "Без имени", 
        "Aucun nom",
        "Kein name", 
        "Sin nombre", 
        "Nenhum nome",
        "لا يوجد اسم", 
        "名前なし", 
        "이름 없음", 
        "没有名字", 
        "Vô danh" 
    };
    public static List<string> topDoubleCoin = new List<string>() 
    { 
        "Double coins", 
        "Удвоить монеты", 
        "Double coin", 
        "Doppelmünze",
        "Moneda doble",
        "Moedas duplas",
        "عملة مزدوجة", 
        "ダブルコイン", 
        "더블 코인",
        "双币", 
        "Gấp đôi xu" 
    };
    public static List<string> topLoading = new List<string>() 
    { 
        "Loading", 
        "Загрузка", 
        "Chargement",
        "Geladen",
        "Cargando", 
        "Carregando",
        "جار التحميل", 
        "積載", 
        "로드 중",
        "载入中",
        "Đang tải" 
    };
    public static List<string> topTopFriend = new List<string>() 
    { 
        "TOP FRIEND", 
        "ТОП ДРУЗЕЙ", 
        "TOP AMI",
        "TOP FREUND",
        "TOP AMIGO",
        "TOP AMIGO", 
        "أعلى صديق", 
        "トップフレンド",
        "톱 친구", 
        "顶部朋友", 
        "BẠN BÈ" 
    };
    public static List<string> topTopCountry = new List<string>() 
    { 
        "TOP COUNTRY", 
        "ТОП СТРАНЫ", 
        "TOP PAYS",
        "TOP LAND",
        "TOP PAÍS",
        "TOP PAÍS", 
        "توب كانتري", 
        "トップカントリー", 
        "톱 국가",
        "顶级国家",
        "QUỐC GIA" 
    };
    public static List<string> topTopWorld = new List<string>() 
    { 
        "TOP WORLD", 
        "ТОП МИРА", 
        "TOP MONDE",
        "TOP WELT",
        "TOP MUNDO",
        "TOP MUNDO", 
        "العالم العلوي", 
        "トップワールド", 
        "톱 월드", 
        "世界顶级",
        "THẾ GIỚI" 
    };
    public static List<string> topTopMultiplayer = new List<string>() 
    { 
        "MULTIPLAYER", 
        "МУЛЬТИПЛЕЕР", 
        "MULTIJOUEUR",
        "MULTIPLAYER",
        "MULTIJUGADOR",
        "MULTIJOGADOR", 
        "المتعدد", 
        "マルチプレイ", 
        "멀티 플레이어", 
        "多人游戏",
        "TRỰC TUYẾN" 
    };
    public static List<string> topLoginFacebook = new List<string>() 
    { 
        "FACEBOOK LOG IN", 
        "ВОЙТИ ЧЕРЕЗ ФЕЙСБУК", 
        "CONNEXION FACEBOOK",
        "EINLOGGEN FACEBOOK",
        "ENTRAR FACEBOOK",
        "ENTRAR FACEBOOK", 
        "تسجيل دخول FACEBOOK", 
        "ログイン FACEBOOK", 
        "로그인 FACEBOOK", 
        "FACEBOOK 登入", 
        "ĐĂNG NHẬP FACEBOOK" 
    };
    //GET XXX COIN RIGHT AWAY
    public static List<string> topNoteGetStart = new List<string>() 
    { 
        "GET", 
        "ПОЛУЧИТЬ", 
        "OBTENIR", 
        "BEKOMMEN",
        "OBTENER", 
        "OBTER", 
        "يحصل على", 
        "取得する", 
        "받다", 
        "得到", 
        "NHẬN" 
    };
    public static List<string> topNoteGetEnd = new List<string>() 
    { 
        "COINS IMMEDIATELY", 
        "МОНЕТ СРАЗУ", 
        "COINS IMMÉDIATEMENT",
        "MÜNZEN SOFORT",
        "MONEDAS INMEDIATAMENTE", 
        "MOEDAS IMEDIATAMENTE", 
        "القطع النقدية فورا", 
        "直ちに 貨幣", 
        "코인 즉시", 
        "立即硬币", 
        "XU NGAY BÂY GIỜ" 
    };

    //IN PAGE SHOP ITEM
    public static List<string> shopButtonBuy = new List<string>() 
    { 
        "BUY", 
        "КУПИТЬ",
        "BUY",
        "KAUF",
        "BUY", 
        "BUY", 
        "يشترى", 
        "買う", 
        "사다", 
        "购买", 
        "MUA" 
    };
    public static List<string> shopBuyItem = new List<string>() 
    { 
        "BUY ITEMS", 
        "КУПИТЬ",
        "ACHETER",
        "KAUFEN",
        "COMPRAR",
        "COMPRAR", 
        "شراء البنود", 
        "商品を 購入する",
        "품목 구입", 
        "购买物品", 
        "MUA VẬT PHẨM" 
    };
    public static List<string> shopUpgrades = new List<string>() 
    { 
        "UPGRADES", 
        "АПГРЕЙДЫ", 
        "AMÉLIORER",
        "VERBESSERN",
        "MEJORAR", 
        "ATUALIZAÇÕES",
        "ترقيات",
        "アップグレード", 
        "업 그레 이드", 
        "升级", 
        "NÂNG CẤP" 
    };
    public static List<string> shopBuyCoin = new List<string>() 
    { 
        "BUY COINS", 
        "КУПИТЬ МОНЕТЫ", 
        "ACHETER COINS", 
        "KAUFE MÜNZEN",
        "COMPRAR MONEDAS",
        "COMPRAR MOEDAS", 
        "شراء عملات",
        "コインを 購入",
        "코인 구매", 
        "买硬币",
        "MUA XU" 
    };
    public static List<string> shopBuyKey = new List<string>() 
    { 
        "BUY KEYS", 
        "КУПИТЬ КЛЮЧИ", 
        "ACHETER KEYS",
        "KAUFE SCHLÜSSEL",
        "COMPRE CLAVES",
        "COMPRAR CHAVES",
        "شراء مفاتيح", 
        "キーを 購入する",
        "키 구입", 
        "买钥匙", 
        "MUA CHÌA KHÓA" 
    };
    public static List<string> shopSingleUse = new List<string>() 
    { 
        "SINGLE USE", 
        "ОДНОРАЗОВЫЙ", 
        "USAGE UNIQUE", 
        "EINZELBENUTZUNG", 
        "USO INDIVIDUAL", 
        "USO ÚNICO",
        "استعمال فردي", 
        "シングルユース", 
        "단일 사용", 
        "单次使用", 
        "HÀNG PHỔ DỤNG" 
    };
    public static List<string> shopTotal = new List<string>() 
    { 
        "Total:", 
        "Всего:", 
        "Total:",
        "Gesamt:",
        "Totalizar:",
        "Total:", 
        "إجمالي:",
        "合計：", 
        "합계:",
        "总：", 
        "Tổng:" 
    };
    public static List<string> shopUseRight = new List<string>() 
    { 
        "Use right after purchase", 
        "Доступно сразу", 
        "Utiliser après l'achat",
        "Benutze nach dem kauf",
        "Uso después compra", 
        "Use depois compra", 
        "استخدام بعد الشراء", 
        "購入後に 使用する", 
        "구매 후 사용", 
        "使用购买后", 
        "Dùng ngay sau khi mua" 
    };
    public static List<string> shopNotEnough = new List<string>() 
    { 
        "Not enough coin", 
        "Недостаточно монет", 
        "Pas assez coin", 
        "Nicht genug münze", 
        "No suficiente moneda", 
        "Não moeda suficiente", 
        "لا يكفي عملة", 
        "コインが 足りない",
        "충분하지 동전", 
        "没有足够的硬币", 
        "Không đủ xu" 
    };
    public static List<string> shopMaxLevel = new List<string>() 
    { 
        "Maximum level", 
        "Максимальный уровень", 
        "Niveau maximum",
        "Maximales level",
        "Nivel máximo", 
        "Nível máximo", 
        "الحد الأقصى", 
        "最大 レベル",
        "최대 레벨", 
        "最大电平",
        "Cấp độ tối đa" 
    };
    public static List<string> shopMaxNumber = new List<string>() 
    { 
        "Maximum number", 
        "Максимальный", 
        "Numéro maximum",
        "Maximale anzahl",
        "Número máximo", 
        "Número máximo", 
        "أقصى عدد", 
        "最大数",
        "최대 수", 
        "最大数量", 
        "Số lượng tối đa" 
    };
    public static List<List<string>> shopPackage = new List<List<string>>() { 
        new List<string>() 
        {
            "SMALL PACKAGE", 
            "МАЛЫЙ ПАКЕТ", 
            "PETIT PAQUET",
            "KLEINES PAKET",
            "PEQUEÑO PAQUETE",
            "PEQUENO PACOTE", 
            "حزمة صغيرة", 
            "小規模 パッケージ", 
            "소규모 패키지", 
            "小包装", 
            "GÓI NHỎ" 
        }, 
        new List<string>() 
        {
            "BIG PACKAGE", 
            "БОЛЬШОЙ ПАКЕТ", 
            "PAQUET NORMAL", 
            "NORMALES PAKET",
            "NORMAL PAQUETE", 
            "NORMAL PACOTE",
            "حزمة عادية", 
            "通常の パッケージ", 
            "일반 패키지",
            "大包装",
            "GÓI THƯỜNG" 
        },
        new List<string>() 
        {
            "LAVISH PACKAGE", 
            "РОСКОШНЫЙ ПАКЕТ", 
            "FORFAIT GRAND", 
            "GROSSES PAKET",
            "GRAN PAQUETE", 
            "GRANDE PACOTE", 
            "حزمة كبيرة",
            "大きな パッケージ", 
            "큰 패키지", 
            "奢华包装",
            "GÓI LỚN" 
        }
    };
    //You will have XXX coins right after the purchase.
    public static List<string> shopStartPurchase = new List<string>() 
    { 
        "You will have", 
        "У тебя будет",
        "Vous aurez", 
        "Du wirst haben", 
        "Usted tendrá", 
        "Voce terá", 
        "سيكون لديك", 
        "あなたは 購入直後に", 
        "당신은 할 것",
        "你将会拥有", 
        "Bạn sẽ có" 
    };
    public static List<string> shopEndCoinPurchase = new List<string>() 
    { 
        "coins right after the purchase.", 
        "монет сразу после покупки.", 
        "coins après l'achat.", 
        "münzen direkt nach dem kauf.", 
        "monedas de la derecha después de la compra.", 
        "moedas direita após a compra.", 
        "القطع النقدية الحق بعد الشراء.", 
        "コインを 持っています。", 
        "구매 후 동전.", 
        "购买后硬币。", 
        "xu ngay sau khi mua hàng." 
    };
    public static List<string> shopEndKeysPurchase = new List<string>() 
    { 
        "keys right after the purchase.", 
        "Ключей сразу после покупки.",
        "keys après l'achat.",
        "schlüssel nach dem kauf.", 
        "teclas de la derecha después de la compra.",
        "chaves direita após a compra.", 
        "مفاتيح الحق بعد الشراء.", 
        "個の キーを 持っています。", 
        "구매 후 열쇠.", 
        "购买后的钥匙。", 
        "chìa khóa ngay sau khi mua hàng." 
    };
    public static List<string> shopHoverboard = new List<string>() 
    { 
        "HOVERBOARD", 
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD", 
        "HOVERBOARD", 
        "HOVERBOARD",
        "HOVERBOARD",
        "HOVERBOARD", 
        "HOVERBOARD",
        "HOVERBOARD", 
        "VÁN BAY" 
    };
    public static List<string> shopChestBox = new List<string>() 
    { 
        "CHEST BOX", 
        "CHEST BOX", 
        "CHEST BOX",
        "CHEST BOX", 
        "CHEST BOX",
        "CHEST BOX", 
        "CHEST BOX", 
        "CHEST BOX",
        "CHEST BOX",
        "CHEST BOX",
        "RƯƠNG BÍ ẨN" 
    };
    public static List<string> shopHeadStart = new List<string>() 
    { 
        "HEADSTART", 
        "HEADSTART", 
        "HEADSTART",
        "HEADSTART",
        "HEADSTART",
        "HEADSTART", 
        "HEADSTART", 
        "HEADSTART",
        "HEADSTART",
        "HEADSTART", 
        "GỌI XE BAY" 
    };
    public static List<string> shopScoreBooster = new List<string>() 
    { 
        "SCOREBOOSTER", 
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER",
        "SCOREBOOSTER", 
        "ĐIỂM CỘNG" 
    };
    public static List<string> shopJetpack = new List<string>() 
    { 
        "JETPACK", 
        "JETPACK", 
        "JETPACK",
        "JETPACK",
        "JETPACK", 
        "JETPACK",
        "JETPACK",
        "JETPACK",
        "JETPACK", 
        "JETPACK",
        "THIẾT BỊ BAY" 
    };
    public static List<string> shopSneaker = new List<string>() 
    { 
        "SUPER SNEAKER", 
        "SUPER SNEAKER",
        "SUPER SNEAKER", 
        "SUPER SNEAKER",
        "SUPER SNEAKER",
        "SUPER SNEAKER",
        "SUPER SNEAKER", 
        "SUPER SNEAKER",
        "SUPER SNEAKER", 
        "SUPER SNEAKER", 
        "GIÀY NHẢY CAO" 
    };
    public static List<string> shopCoinMagnet = new List<string>() 
    { 
        "COIN MAGNET", 
        "COIN MAGNET", 
        "COIN MAGNET", 
        "COIN MAGNET", 
        "COIN MAGNET", 
        "COIN MAGNET", 
        "COIN MAGNET",
        "COIN MAGNET", 
        "COIN MAGNET", 
        "COIN MAGNET", 
        "NAM CHÂM" 
    };
    public static List<string> shop2xMultiplier = new List<string>() 
    { 
        "2X MULTIPLIER", 
        "2X MULTIPLIER",
        "2X MULTIPLIER", 
        "2X MULTIPLIER",
        "2X MULTIPLIER", 
        "2X MULTIPLIER",
        "2X MULTIPLIER", 
        "2X MULTIPLIER",
        "2X MULTIPLIER", 
        "2X MULTIPLIER", 
        "NHÂN ĐÔI ĐIỂM" 
    };
    public static List<string> shopHoverbike = new List<string>() 
    { 
        "HOVERBIKE", 
        "HOVERBIKE", 
        "HOVERBIKE", 
        "HOVERBIKE", 
        "HOVERBIKE",
        "HOVERBIKE",
        "HOVERBIKE", 
        "HOVERBIKE", 
        "HOVERBIKE", 
        "HOVERBIKE",
        "XE MÁY BAY" 
    };
    //Increase time to use XXXXX
    public static List<string> shopDetailUpgrades = new List<string>() 
    { 
        "Increase time to use", 
        "Увеличить время действия", 
        "Augmenter le temps d'utiliser", 
        "Erhöhen sie zeit zu nutzen",
        "Aumente el tiempo de uso", 
        "Aumente o tempo de uso", 
        "زيادة الوقت للاستخدام", 
        "使用時間を 増やす", 
        "사용 시간 증가",
        "增加使用时间", 
        "Tăng thời gian sử dụng" 
    };
    public static List<string> shopDetailHoverboard = new List<string>() 
    { 
        "If you hit an object while riding a hoverboard, your run won't end.", 
        "При столкновении с препятствиями, доска позволит не упасть и продолжить бег.", 
        "Quand entrer en collision avec les obstacles, il vous aidera à ne pas tomber.", 
        "Wenn du mit hindernissen kollidierst, wird es dir helfen, nicht zu fallen.", 
        "Al chocar con obstáculos, le ayudará a no caer.", 
        "Ao colidir com obstáculos, isso irá ajudá-lo a não cair.",
        "عند الاصطدام مع العقبات، وسوف تساعدك على عدم الوقوع.",
        "障害物に 衝突すると、 落ちないようになります。", 
        "장애물과 충돌하면 넘어지지 않게됩니다.", 
        "如果您在 骑着悬 浮盘时碰 到物体， 您的跑步将 不会结束。", 
        "Khi va chạm với các vật cản, nó giúp bạn không bị ngã." 
    };
    public static List<string> shopDetailChestbox = new List<string>() 
    { 
        "It is a mystery gift box, with ramdom prizes inside, such as, coins, keys, hoverboards and more.", 
        "Это загадочная коробочка-подарок, внутри могут быть монеты, ключи, доски и многое другое.",
        "C'est une boîte cadeau mystérieuse, à l'intérieur peut être des pièces de monnaie, des clés, HOVERBOARD et plus encore.",
        "Es ist eine geheimnisvolle geschenkbox, innen kann es münzen, schlüssel, HOVERBOARD und vieles mehr sein.",
        "Es una caja de regalo misteriosa, dentro de ella puede ser monedas, llaves, HOVERBOARD y más.", 
        "É uma misteriosa caixa de presente, dentro pode ser moedas, chaves, HOVERBOARD e muito mais.", 
        "بل هو مربع هدية غامضة، داخله يمكن أن يكون النقود، والمفاتيح، HOVERBOARD وأكثر من ذلك.", 
        "それは、 コイン、 キー、 HOVERBOARD ともっとすることができ 内部では、 神秘的な ギフトボックスです。", 
        "그것은 신비한 선물 상자이며 동전, 열쇠, HOVERBOARD 이상이 될 수 있습니다.", 
        "这是一个 神秘的 礼品盒， 里面有随 机的奖品， 如硬币， 钥匙， 悬浮板等等。", 
        "Nó là hộp quà bí mật, bên trong nó có thể là xu, chìa khóa, ván bay và nhiều thứ khác." 
    };
    public static List<string> shopDetailHeadstart = new List<string>() 
    { 
        "Lets you use HOVERBIKE at any time.", 
        "Позволяет использовать летоцыкл в любое время.", 
        "Vous permet d'utiliser HOVERBIKE à tout moment.", 
        "Ermöglicht es ihnen, HOVERBIKE jederzeit zu benutzen.", 
        "Permite utilizar HOVERBIKE en cualquier momento.", 
        "Permite usar o HOVERBIKE a qualquer momento.", 
        "يتيح لك استخدام HOVERBIKE في أي وقت.",
        "HOVERBIKE をいつでも 使用できます。", 
        "언제든지 HOVERBIKE 를 사용할 수 있습니다.", 
        "让您随 时使用 HOVERBIKE。", 
        "Nó cho phép bạn sử dụng Xe Máy Bay bất cứ khi nào bạn muốn." 
    };
    public static List<string> shopDetailScoreBooster = new List<string>() 
    { 
        "Boost your score while running.", 
        "Приумнож свой счет на бегу.", 
        "Augmentez votre score sur la course.", 
        "Steigere deine punktzahl auf der flucht.", 
        "Aumente su puntuación en la carrera.", 
        "Aumente sua pontuação em fuga.",
        "تعزيز درجاتك على المدى.", 
        "実行時にあなたの スコアを 後押し。",
        "실행에 점수를 향상.", 
        "在跑步 时提高 你的分数。", 
        "Tăng cường điểm số trong khi chạy." 
    };

    //IN OTHER PAGE
    public static List<string> otherNewHighScore = new List<string>() 
    { 
        "NEW HIGH SCORE", 
        "НОВЫЙ РЕКОРД", 
        "NOUVEAU POINT ÉLEVÉ",
        "NEUES HOCHSPIEL", 
        "NUEVA PUNTUACIÓN", 
        "NOVA PONTUAÇÃO", 
        "درجة عالية جديدة", 
        "新 ハイスコア",
        "새로운 최고 점수", 
        "新高分", 
        "ĐIỂM KỶ LỤC MỚI" 
    };
    public static List<string> otherTapContinue = new List<string>() 
    { 
        "Tap to continue", 
        "Нажми, чтобы продолжить", 
        "Appuyez pour continuer", 
        "Tippen um fortzufahren", 
        "Pulse para continuar",
        "Clique para continuar", 
        "إضغط للإستمرار", 
        "タップして 続行する", 
        "계속하려면 탭하세요",
        "点按继续", 
        "Chạm để tiếp tục" 
    };
    public static List<string> otherTapOpen = new List<string>() 
    { 
        "Tap to open!", 
        "Нажми, чтобы открыть!", 
        "Appuyez pour ouvrir!", 
        "Tippen sie zu öffnen!", 
        "Toque para abrir!", 
        "Toque para abrir!", 
        "اضغط للفتح!", 
        "タップして 開く！",
        "탭하여여십시오!",
        "点击打开！", 
        "Chạm để mở hộp!" 
    };
    public static List<List<string>> otherNameItemBonus = new List<List<string>>() { 
        new List<string>() 
        {
            "COIN PACKAGE", 
            "ПАКЕТ МОНЕТ", 
            "FORFAIT COIN", 
            "MÜNZENPAKET", 
            "PAQUETE MONEDA",
            "PACOTE MOEDA", 
            "حزمة عملة", 
            "コインパッケージ",
            "코인 패키지", 
            "硬币包",
            "TÚI XU" 
        }, 
        new List<string>() 
        {
            "KEY PACKAGE", 
            "ПАКЕТ КЛЮЧЕЙ", 
            "FORFAIT CLÉ", 
            "SCHLÜSSELPAKET", 
            "PAQUETE CLAVE", 
            "PACOTE CHAVE", 
            "حزمة مفتاح", 
            "キーパッケージ", 
            "키 패키지",
            "密钥包",
            "CHÌA KHÓA" 
        },
        new List<string>() 
        {
            "HOVERBOARD", 
            "HOVERBOARD",
            "HOVERBOARD", 
            "HOVERBOARD", 
            "HOVERBOARD",
            "HOVERBOARD", 
            "HOVERBOARD",
            "HOVERBOARD", 
            "HOVERBOARD", 
            "HOVERBOARD",
            "VÁN BAY" 
        },
        new List<string>() 
        {
            "HEADSTART", 
            "HEADSTART", 
            "HEADSTART", 
            "HEADSTART", 
            "HEADSTART",
            "HEADSTART",
            "HEADSTART", 
            "HEADSTART",
            "HEADSTART", 
            "HEADSTART", 
            "GỌI XE BAY"
        },
        new List<string>() 
        {
            "SCOREBOOSTER", 
            "SCOREBOOSTER",
            "SCOREBOOSTER", 
            "SCOREBOOSTER",
            "SCOREBOOSTER",
            "SCOREBOOSTER",
            "SCOREBOOSTER",
            "SCOREBOOSTER", 
            "SCOREBOOSTER", 
            "SCOREBOOSTER",
            "ĐIỂM CỘNG" 
        } 
    };
    public static List<List<string>> otherInfoItemBonus = new List<List<string>>() { 
        new List<string>() 
        {
            "You can use it to shop for items and upgrades.", 
            "Можно использовать для покупок предметов и апгрейдов.",
            "Vous pouvez l'utiliser pour acheter des articles et mettre à niveau des éléments.", 
            "Sie können es verwenden, um gegenstände zu kaufen und artikel zu aktualisieren.",
            "Usted puede utilizarlo para comprar artículos y artículos de la mejora.", 
            "Você pode usá-lo para comprar itens e atualizar itens.",
            "يمكنك استخدامه لشراء سلع وترقية العناصر.", 
            "あなたはそれを 使って アイテムを 購入し、 アイテムを アップグレードすることができます。",
            "당신이 항목을 구입하고 업그레이드하는 데 사용할 수 있습니다.", 
            "您可以 使用它 来购买物 品和升级。", 
            "Bạn có thể sử dụng nó để mua và nâng cấp các vật phẩm." 
        }, 
        new List<string>() 
        {
            "Helps you revive when you crash.", 
            "Позволит продолжить игру если произошло столкновение.", 
            "Vous aide à relancer lorsque vous vous écrasez.", 
            "Hilft euch zu beleben, wenn ihr abstürzt.", 
            "Le ayuda a revivir cuando se bloquea.", 
            "Ajuda você a reviver quando você falhar.", 
            "يساعدك على إحياء عندما تحطم.", 
            "あなたが 墜落したときにあなたを 復活させるのに 役立ちます。", 
            "당신이 충돌 할 때 회복하는 데 도움이됩니다.", 
            "帮助你 在崩溃时 恢复活力。", 
            "Giúp bạn tiếp tục chạy khi bạn ngã." 
        },
        new List<string>() 
        {
            "Double-click to use when you are running.", 
            "Нажми дважды, чтобы использовать, на бегу.", 
            "Double-cliquez pour utiliser lorsque vous exécutez.",
            "Doppelklicken sie, um zu verwenden, wenn sie laufen.",
            "Haga doble clic para utilizar cuando se ejecuta.",
            "Clique duas vezes para usar quando você estiver executando.",
            "انقر نقرا مزدوجا فوق لاستخدام عند تشغيل.",
            "あなたが 実行時に 使用する ダブルクリックします。",
            "당신이 실행할 때 사용할 두 번 클릭합니다.", 
            "双击以 在运行 时使用。", 
            "Chạm 2 lần để sử dụng khi bạn đang chạy." 
        },
        new List<string>() 
        {
            "Lets you use HOVERBIKE at any time.", 
            "Позволяет использовать ЛЕТОЦИКЛ в любое время.", 
            "Vous permet d'utiliser HOVERBIKE à tout moment.", 
            "Ermöglicht es Ihnen, HOVERBIKE jederzeit zu benutzen.",
            "Permite utilizar HOVERBIKE en cualquier momento.",
            "Permite usar o HOVERBIKE a qualquer momento.",
            "يتيح لك استخدام HOVERBIKE في أي وقت.", 
            "HOVERBIKE をいつでも 使用できます。", 
            "언제든지 HOVERBIKE 를 사용할 수 있습니다.",
            "让您随 时使用 HOVERBIKE。",
            "Nó cho phép bạn sử dụng Xe Máy Bay bất cứ khi nào bạn muốn."
        },
        new List<string>() 
        {
            "Boost your score on the go.", 
            "Приумнож свой счет на бегу.", 
            "Augmentez votre score sur la route.", 
            "Steigern sie ihre punktzahl unterwegs.",
            "Aumente su puntuación en el camino.",
            "Aumente sua pontuação em movimento.",
            "تعزيز درجاتك على الذهاب.",
            "外出先であなたの スコアを 後押し。", 
            "이동 중에도 점수를 높일 수 있습니다.",
            "提高你 的分数 在旅途中。", 
            "Tăng cường điểm số trong khi chạy." 
        }
    };

    //IN NOTIFICATIONS
    public static List<string> notifiTitle = new List<string>() 
    { 
        gameName, 
        gameName, 
        gameName,
        gameName, 
        gameName, 
        gameName,
        gameName, 
        gameName, 
        gameName, 
        gameName, 
        gameName 
    };
    public static List<string> notifiContent = new List<string>() 
    { 
        "You can be the best! Hurry back and put your skills at test!", 
        "Ты можешь стать лучшим! Возвращайся поскорее и докажи это всем!", 
        "Tu peux être le meilleur! Dépêchez-vous et mettez vos compétences à l'épreuve!", 
        "Du kannst der beste sein! Beeilen sie sich und setzen sie ihre fähigkeiten auf probe!", 
        "Usted puede ser el mejor! Date prisa y pon tus habilidades a prueba!", 
        "Você pode ser o melhor! Apresse-se de volta e coloque suas habilidades em teste!", 
        "يمكنك أن تكون أفضل! عجلوا ووضع مهاراتك في الاختبار!", 
        "あなたは 最高になることができます！ 急いで テストにあなたの スキルを 入れてください！", 
        "당신은 최고가 될 수 있습니다! 다시 서둘러 테스트에서 실력을 넣어!", 
        "你可以是最好的！ 快点回来， 把你的技能测试！", 
        "Bạn có thể là người chơi tốt nhất! Hãy quay lại và trải nghiệm!" 
    };
}
