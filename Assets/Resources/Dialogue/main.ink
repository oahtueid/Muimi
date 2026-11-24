VAR ann_trust=0
EXTERNAL AddObjective(id)
EXTERNAL CompleteObjective(id)
EXTERNAL StartMinigame(name)
EXTERNAL PlayCutscene(name)
\ #effect:flashback
"Are you happy?" #speaker:??? #image:hidden_face
"..." #speaker:me
"What if you can start over?" #speaker:???
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
~PlayCutscene("AnnComing")
~ AddObjective(1)
"Morningg! Get all ready?" #speaker:???

"... hello"

Still feeling exhausted, I offered a faint greeting in return. But… who is she, exactly? From what I’ve gathered, it seems like a classmate has come to pick me up for school—and that loud voice from earlier… could that have been my mother? But why don’t I recognize the voices of my own mother or my friend? Who… am I? #speaker:none #effect:flash
"Urgh." #speaker:Mokusa #potrait:Mokusa/pain
"..." #speaker:Ann #potrait:Ann/normal
"Did you forget to take your medicine again?"
"Medicine?" What medicine?
"Hmm? You know, the medicine for your headaches. You’ve been suffering from headaches and insomnia lately, so you went to the pharmacy to get some, remember?"
"Oh… I see. I suppose that’s right…"
"Are you alright? Are you still up for going to school?" #speaker:Ann #potrait:Ann/worried
* [Tell her about my current state]
-> tellAnnCondition
* [Reassure her that I’m fine and can still go to school]
-> hideAnnAndGoToSchool
* [Say that I’m not feeling well and decide to stay home]
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
The new school didn’t have many trees yet, so the courtyard was always bathed in sunlight—especially in the afternoon. There were three buildings, but only one contained classrooms; since it was only two stories high, the different grade levels had to split the day, attending in either the morning or afternoon shifts. I recognized this place—I had definitely studied here before. I followed Ann into the classroom—the first one in the row on the first floor. Ann and I sat side-by-side at the fourth desk in the second row from the door. To my surprise, I was able to find my seat without having to ask anyone; perhaps this is what they call "muscle memory."
Not long after we arrived, the bell rang for class to begin. The boy sitting in front of us turned around.
"Hello. Did you study?" he asked, looking quite relaxed. I knew him. He was…
"Morning, Kento. I studied a bit. Mokusa didn’t even remember there was a test today!"
"Eh? Are you going to be okay?" His expression mirrored Ann’s—a mix of surprise and concern.
"I’ll be fine. I’m not too worried about English."
"Yeah, that’s true—English probably won’t give you any trouble."
I nodded. I was quite confident in my English; a standard classroom test wouldn’t be an issue at all.
…
He really was Kento—I had definitely been in class with him before. And the other students looked incredibly familiar, too. Perhaps my memories weren’t entirely gone; maybe they could still be recovered.
*Bing-bong.*
The bell rang, signaling the end of the homeroom period. It was almost time for the test. Once the papers had been handed out and the teacher had finished explaining the exam, the bell rang once more—this time, marking the start of the test.
-> END
= AfterExam1
As the test concluded and all papers were submitted, the class said goodbye to the teacher, and everyone began comparing their answers.
"ABDC."
"Correct."
"CADB."
"Correct."
"Wait, isn't question 7 'A'?"
"What? What did you guys get?"
"D."
"D."
"D."
"Nooo!"
"Better luck next time."
One soldier down. As for me, I started checking my answers with Kento and Ann.

->END
-> END

=== tellAnnCondition === 
~ann_trust = ann_trust + 5
I explained my situation to her... #speaker:narrator
"Urgh, I don’t even know what to say. Does your family know about this? Well, judging by your reaction, I guess not."
"Yeah, I only just realized it myself a little while ago."
"For now, let’s just head to school. I’ll fill you in on the way—maybe it’ll help jog your memory."
"Right."
I got dressed, tidied myself up, and prepared to head to school. #speaker:narrator
"Last week, you mentioned having a headache and trouble sleeping, so I went with you to the pharmacy to get something for the pain. You seemed better after a few days, so I felt a bit relieved—but now this happens! I wonder if you stopped taking the medicine, or if there was something wrong with it..."
"Do you know why I had those headaches?"
"You said you were stressed, but you didn't go into detail. You seemed really hesitant, so I told you you could tell me whenever you felt ready."
"Oh, and you probably don't remember me yet, do you? I’m Ann—we’re best friends!"
"Really?"
"Yeah!"
"I’m sorry, I still can’t recall anything at all."
"It’s okay, don’t worry about it! Maybe spending a day at school will help bring some memories back."
Opening up to her helped me calm down a bit. I’m glad I decided to tell her.
"Ah." Suddenly, as if something just occurred to her, Ann turned toward me, looking a bit hesitant.
"Oh, wait—we have our final English exam coming up..."
"EH!?"
~ AddObjective(2)
-> END

=== hideAnnAndGoToSchool ===
"Sure, just give me a moment to get ready."
I changed my clothes, tidied myself up, and got ready for school… #speaker:narrator
"By the way, have you studied for the test?"
"Huh? What test?"
"What?" Ann’s eyes went wide, her mouth falling open.
"The final English exam today—don't tell me you forgot?"
"EH?!" This time, I couldn't help but gape in shock.
"Are you serious?"
We stared at each other.
…
"You’ve been acting a bit strange all morning. Are you sure you’re okay?"
"I’m fine—I think I’m just not fully awake yet."
"Hmm." Ann narrowed her eyes, staring straight at me. It’s no wonder she’s suspicious—why on earth did the test have to fall on a day like this? ;-;
"Anyway, like I said before, I’ll wait. Just tell me whenever you feel comfortable enough to talk."
"Before?" I muttered.
"Hmm?"
"Oh, never mind. Thanks a lot."
"Don't mention it! We’re best friends, after all."
"And just in case you’re still not quite awake—the test is during the first period, by the way."
"..."
~ AddObjective(2)
-> END

=== hideAnnAndNotGoToSchool ===
"Wait, seriously? Oh man, and there’s an exam today, too!" #speaker:Ann #potrait:Ann/confused
"An exam?!"
"Yeah—don't tell me you forgot? It’s the final English exam today."
"A FINAL EXAM!?"
"Mhm. You could ask for an excused absence, but you’d have to notify the teacher and wait for them to prepare a different version of the test for you to take later."
…Of course, the timing just had to be perfect. I guess I’ll just have to go; otherwise, it’ll just lead to a whole lot of extra hassle. Besides, it’s English—I think I can manage.
"...Alright, I guess I’ll go after all."
"Are you sure you’re okay?"
"Yeah, I’m fine."
"Well, the exam is during the first period—you could just finish that and then head home to rest."
"Mhm."

-> END


=== MusicClub ===
Play music? #image:none
+ [Yes]
    ~StartMinigame("Rhythm")
    ->DONE
+ [No]
->DONE

-> END
