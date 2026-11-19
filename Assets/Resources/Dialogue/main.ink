VAR ann_trust=0
EXTERNAL AddObjective(id)
EXTERNAL CompleteObjective(id)
EXTERNAL StartMinigame(name)
\ #effect:flashback
"Are you happy?" #speaker:???
"..." #speaker:me
"What if you can start over?" #speaker:??? #image:hidden_face
\ #effect:wake_up
-> Day1


=== Day1 ===
= Scene1

"Strange dream" #speaker:me #effect:none

Feel nostalgic, as if I know her before long
Look around, strange feeling bottle up in me

"MOKUSA, GET UP! Ann-chan comes" #speaker:??? #effect:loud
!?
#sound:bang #image:none
~ AddObjective(1)
"Morningg! Get all ready?" #speaker:???

"... hello"

Vẫn chưa hết mệt mỏi, tôi khẽ chào lại. Nhưng mà cô ấy là ai vậy chứ. Từ những gì nghe được, thì có vẻ đây là bạn học của mình tới đón mình đi học, và giọng lớn hồi nãy có thể là mẹ mình? Nhưng tại sao mình lại không biết giọng mẹ và bạn mình chứ. Mình là.. ai? #speaker:none #effect:flash
"Urgh" #speaker:Mokusa #potrait:Mokusa/pain
"..." #speaker:Ann #potrait:Ann/normal
"Qua cậu quên uống thuốc à.."
"Thuốc?" Thuốc gì?
"Hửm? Thì là thuốc đau đầu đó, mấy nay cậu hay đau đầu mất ngủ nên đi hiệu thuốc mua thuốc còn gì."
"Vậy… à. Chắc là vậy thật…"
"Cậu ổn không, vẫn đi học được chứ?" #speaker:Ann #potrait:Ann/worried
* [Nói cho cô ấy tình trạng của mình]
    -> tellAnnCondition
* [Trấn an rằng mình không sao, vẫn đi học được]
    -> hideAnnAndGoToSchool
* [Nói rằng cảm thấy không ổn nên xin nghỉ]
    -> hideAnnAndNotGoToSchool

= School
// "Lucky we get it in time" #speaker:Ann #image:none
// "*Ring ring" #sound:bell
// "Why are you still standing there, dont tell me you forgot your seat too? Here, next to me" #speaker:Ann
// "What is it guys?" #speaker:???
// "She's today not feeling really well" #speaker:Ann
// "Are you alright, we're going to have test soon. can you do it?" #speaker:???
// "Dont worry. I think i'll be alright"
// "Yeah, that's really just you" #speaker:???
// "*Ring ring" #sound:bell
// "Right. As announce last week, today we'll have a test, close all your books please"
Ngôi trường mới, chưa có quá nhiều cây nên sân lúc nào cũng nắng đặc biệt vào trưa chiều. Có 3 tòa nhà, nhưng chỉ có 1 tòa là các lớp học, chỉ có 2 tầng nên các khối chia nhau học sáng chiều… Ngôi trường này, mình biết về nó, chắc chắn mình đã có học ở đây. Tôi theo Ann vào lớp, đó là lớp đầu dãy ở tầng 1. Tôi với Ann ngồi cạnh nhau ở bàn thứ 4 dãy thứ 2 tính từ cửa vào. Ngạc nhiên là tôi có thể tới đúng chỗ ngồi của mình mà không phải hỏi ai, có lẽ đây gọi là trí nhớ cơ bắp.
Tụi tôi tới được một lúc thì tiếng chuông vào lớp kêu lên. Cậu trai ngồi phía trên quay xuống bàn
"Hello. Ôn bài chưa đó?" Cậu trai hỏi, trông có vẻ thoải mái. Mình biết cậu ta. Cậu ta là….
"Morning, Kento. Cũng ôn một ít. Mokusa còn k nhớ nay kiểm tra nữa cơ KD"
"Eh, ổn không vậy?" Biểu cảm của cậu ta y hệt như Ann, ngạc nhiên xen lẫn lo lắng
"Ổn mà, tiếng Anh thì không lo lắm"
"Uhm, đúng vậy thật, tiếng Anh thì chắc không làm khó cậu được đâu"
Tôi gật đầu. Tôi khá tự tin về tiếng Anh của mình, nếu chỉ là bài kiểm tra trên lớp thì không vấn đề gì.
…
Cậu ta đúng là Kento, mình đã từng học với cậu ta. Và những bạn trong lớp trông cũng cực kỳ quen. Có lẽ ký ức của mình không hoàn toàn mất đi, vẫn có thể lấy lại được.
Bing boongg
Tiếng chuông kết thúc tiết chủ nhiệm vang lên. Sắp tới giờ kiểm tra rồi. Sau khi giấy được phát xong xuôi, giáo viên giải thích kỳ thi, tiếng chuông vang lên lần nữa, lần này là bắt đầu tính giờ làm bài.
-> END
= AfterExam1
Kết thúc kiểm tra, các bài nộp xong xuôi, cả lớp chào cô, mọi người bắt đầu so đáp án với nhau
"ABDC"
"Đúng"
"CADB"
"Đúng"
"Khoan, câu 7 A chứ!"
"Gì, tụi mày ra nhiêu"
"D"
"D"
"D"
"Nooo"
"Chúc may mắn lần sau"
1 chiến sĩ hy sinh. Về phần mình, tôi cũng dò đáp án với Kento và Ann.

->END
-> END

=== tellAnnCondition === 
~ann_trust = ann_trust + 5
Tôi giải thích tình trạng của mình với cô ấy...#speaker:narrator
"Urgh, mình không biết nói gì hơn. Nhà cậu có biết chuyện này không. Mà, theo phản ứng của cô thì chắc là không rồi"
"Uhm, mình mới nhận ra vừa nãy thôi"
"Chắc là trước mắt mình cứ đi học đã, trong lúc đi tớ sẽ kể lại cho cậu, xem cậu có nhớ lại được không"
"Uhm"
Tôi thay quần áo, chải chuốt và chuẩn bị đi học. #speaker:narrator
"Tuần trước cậu nói là mình đau đầu, k ngủ được nên mình cùng đi với cậu tới hiệu thuốc để bớt đau hơn. Được vài ngày thì nhìn cậu ổn hơn nên mình cũng yên tâm phần nào, ai ngờ giờ lại thành như này, không biết là do không uống thuốc, hay thuốc có vấn đề nữa… "
"Tại sao mình đau đầu, cậu có biết không?"
"Cậu có nói rằng mình stress, nhưng không nói rõ lý do. Lúc đó trông cậu chần chừ lắm, nên mình mới nói là khi nào sẵn sàng thì kể cho mình nghe cũng được."
"À mà chắc cậu chưa nhớ ra tớ nhỉ. Tớ là Ann, tụi mình là bạn thân á"
"Thật hả"
"Uhm!"
"Xin lỗi, tớ chưa nhớ ra được gì hết"
"Không sao, không sao. Biết đâu trải qua 1 ngày trên trường có khi lại giúp cậu nhớ gì đó thì sao"
Nhờ trải lòng với cậu ấy, tôi bình tĩnh lại được phần nào. Thật tốt khi quyết định nói.
"Ah". Như vừa nghĩ ra gì đó, Ann liền quay sang tôi, ngập ngừng.
"Quên mất, tí nữa kiểm tra tiếng Anh cuối kỳ á …"
"EH!?"
~ AddObjective(2)
-> END

=== hideAnnAndGoToSchool ===
"Ổn mà, cậu đợi chút mình chuẩn bị nha"
Tôi thay quần áo, chải chuốt và chuẩn bị đi học….
[Kiểm tra 2]
"À mà, cậu ôn bài chưa?"
"Eh, bài gì cơ?"
"Hả" Ann tròn mắt, há hốc mồm
"Nay kiểm tra cuối kỳ tiếng Anh á, cậu không nhớ hả?"
"EH?!" Lần này đến lượt tôi không khỏi kiềm được mà há mồm
"Thật luôn á hả?"
Hai đứa nhìn nhau
…
"Sáng h trông cậu cứ kỳ lạ sao ấy, cậu chắc là mình ổn chứ"
"Ổn mà, mình nghĩ là do mình chưa tỉnh ngủ hẳn thôi"
"Hmm" Ann nheo mắt nhìn chằm chằm tôi. Cậu ấy nghi ngờ cũng phải thôi, sao lại nhằm lúc này có kiểm tra cơ chứ ;-;
"Mà, như tớ đã nói lúc trước, tớ sẽ đợi, đến khi nào cậu thấy thoải mái thì hãy nói với tớ"
"Lúc trước?". Tôi lẩm bẩm
"Hmm?"
"À, không có gì. Cảm ơn cậu nhiều"
"Có gì đâu, tụi mình bạn thân mà."
"Mà đề phòng quý cô đây chưa tỉnh ngủ hẳn thì kiểm tra vào tiết 1 á nha"
"..."
~ AddObjective(2)
-> END

=== hideAnnAndNotGoToSchool ===
"Eh, thật à. Toang rồi, nay còn có kiểm tra nữa chứ" #speaker:Ann #potrait:Ann/confused
"Kiểm tra?!"
"Ừ, ủa cậu không nhớ hả, nay kiểm tra cuối kỳ tiếng Anh đó"
"CUỐI KỲ!?"
"Uhm. Xin nghỉ cũng đc, nhma phải báo lại với giáo viên rồi còn đợi chuẩn bị đề khác để làm nữa á."
…Đúng lúc quá vậy trời. Chắc ráng đi vậy, nếu không thì phát sinh thêm nhiều thứ phiền phức. Với lại, nếu là tiếng Anh thì tôi nghĩ mình xoay sở được.
"...Đành vậy, chắc tớ vẫn sẽ đi"
"Ổn không đó"
"Ổn mà"
"Mà, kiểm tra vào tiết 1, ráng kiểm tra xong rồi về nghỉ cũng được"
"Uhm"

-> END


=== MusicClub ===
Play music? #image:none
+ [Yes]
    ~StartMinigame("Rhythm")
    ->DONE
+ [No]
->DONE

-> END
