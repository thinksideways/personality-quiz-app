using System.Collections.Generic;

namespace personality_quiz.Models;

public class Quiz : List<Question>
{
    public Quiz()
    {
        AddRange(new List<Question>
        {
            new Question { Text = "I prefer quiet nights in.", IntrovertIfTrue = true },
            new Question { Text = "Large groups energize me.", IntrovertIfTrue = false },
            new Question { Text = "I think before I speak.", IntrovertIfTrue = true },
            new Question { Text = "I enjoy the spotlight.", IntrovertIfTrue = false },
            new Question { Text = "Small talk exhausts me.", IntrovertIfTrue = true },
            new Question { Text = "I talk to strangers often.", IntrovertIfTrue = false },
            new Question { Text = "I prefer working alone.", IntrovertIfTrue = true },
            new Question { Text = "I decide using logic.", IntrovertIfTrue = true },
            new Question { Text = "I am a 'people person'.", IntrovertIfTrue = false },
            new Question { Text = "I need alone time to recharge.", IntrovertIfTrue = true },
            new Question { Text = "I love trying new things.", IntrovertIfTrue = false },
            new Question { Text = "I have a small friend circle.", IntrovertIfTrue = true },
            new Question { Text = "I am comfortable in crowds.", IntrovertIfTrue = false },
            new Question { Text = "I enjoy deep conversations.", IntrovertIfTrue = true },
            new Question { Text = "I often act on impulse.", IntrovertIfTrue = false },
            new Question { Text = "I am a good listener.", IntrovertIfTrue = true },
            new Question { Text = "I enjoy public speaking.", IntrovertIfTrue = false },
            new Question { Text = "I prefer planning things out.", IntrovertIfTrue = true },
            new Question { Text = "I express feelings easily.", IntrovertIfTrue = false },
            new Question { Text = "I value my privacy highly.", IntrovertIfTrue = true }
        });
    }
}
