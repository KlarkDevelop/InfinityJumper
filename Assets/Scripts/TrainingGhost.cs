using TMPro;
using UnityEngine;

public class TrainingGhost : MonoBehaviour
{
    public TMP_Text text;
    public Animator speakAnim;
    private SpriteRenderer spriteSpeak;
    public SpriteRenderer cloudText;
    public SaveSystem save;

    [Header("Joysticks")]
    public GameObject jumpJoy;
    public GameObject dashJoy;
    public GameObject throwJoy;

    [Header("Show area joysticks")]
    public SpriteRenderer jumpArea;
    public SpriteRenderer throwArea;
    public SpriteRenderer dashArea;

    private void Awake()
    {
        enebleControl(false);
        spriteSpeak = speakAnim.GetComponent<SpriteRenderer>();
        jumpArea.gameObject.SetActive(false);
        throwArea.gameObject.SetActive(false);
        dashArea.gameObject.SetActive(false);
    }

    private bool click = false;

    private int step = 1;
    private int replicNum = 1;

    private bool workFirstReplic = true;
    private void Update()
    {
        if (controlEnabled == false)
        {
            if (save.lang == "en" && workFirstReplic)
            {
                changeTextInCloud("Welocome to the cursed castle and bla bla bla...");
                workFirstReplic = false;
            }
            else if (save.lang == "ru" && workFirstReplic)
            {
                changeTextInCloud("����� ���������� � ��������� ����� � ��� ��� ���...");
                workFirstReplic = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                replicNum++;
                click = true;
            }

            //For Editor
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                replicNum +=100;
                click = true;
            }
            //For Editor

            if (step == 1 && click)
            {
                if(save.lang == "en")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("Welocome to the cursed castle and bla bla bla..."); break;
                        case 2: changeTextInCloud("I actually have to not let you go here."); break;
                        case 3: changeTextInCloud("But i'm tired of daily visitors"); break;
                        case 4: changeTextInCloud("Anyway, no one can walk to the treasure"); break;
                        case 5: changeTextInCloud("But the robbers love to collect coins on the way"); break;
                        case 6: changeTextInCloud("I closed you here that you would get rid of them"); break;
                        case 7: changeTextInCloud("In return, i will strengthen you, for assembly coins"); break;
                        case 8: changeTextInCloud("I will teach you how to survive here"); break;
                        case 9: changeTextInCloud("Well, firstly, you can not walk, i do not know why"); break;
                        case 10: changeTextInCloud("But you can jump and slide"); break;
                        case 11: changeTextInCloud("I'll give you the opportunity to do a double jump"); break;
                        case 12: changeTextInCloud("Go ahead, i will catch up"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }else if(save.lang == "ru")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("����� ���������� � ��������� ����� � ��� ��� ���..."); break;
                        case 2: changeTextInCloud("������, � �� ������ ���� �������"); break;
                        case 3: changeTextInCloud("�� � ��� ����� �� ���������� �����������"); break;
                        case 4: changeTextInCloud("� ����� ������, ����� �� ����� ����� �� ���������"); break;
                        case 5: changeTextInCloud("�� ���������� ����� �������� ������ �� ����"); break;
                        case 6: changeTextInCloud("� ���� ������ ������ ��� ��� �� �� �� ��� ���������"); break;
                        case 7: changeTextInCloud("� ����� � ������ ���� �������, �� ������ �����������"); break;
                        case 8: changeTextInCloud("� ���� ������� ��� ��� ��������"); break;
                        case 9: changeTextInCloud("��, ��-������, ��� ������ ��������� ������..."); break;
                        case 10: changeTextInCloud("�� �� ������ ������� � ���������"); break;
                        case 11: changeTextInCloud("� ���� ���� ����������� ������ ������� ������"); break;
                        case 12: changeTextInCloud("��� ������, � ������"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }
                
            }else if (step == 2 && click)
            {
                if(save.lang == "en")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("This thing replenishes your ammo and dash"); break;
                        case 2: changeTextInCloud("Now it will give you only ammo"); break;
                        case 3: changeTextInCloud("Throw your shuriken at the button"); break;
                        case 4: changeTextInCloud("You can also press it yourself"); break;
                        case 5: changeTextInCloud("You're a ninja, you should be able to jump on the walls"); break;
                        case 6: changeTextInCloud("And behind this door you need to slide on the flor"); break;
                        case 7: changeTextInCloud("You can increase the speed in the process"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }else if(save.lang == "ru")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("��� ����� �������� ���� ����� �������� � ������"); break;
                        case 2: changeTextInCloud("� ������ ������ ��� ������ ������ �������"); break;
                        case 3: changeTextInCloud("������� ��� ����� � ���� ���� ������� � ������"); break;
                        case 4: changeTextInCloud("��� �� �� ������ ������ ������ ���"); break;
                        case 5: changeTextInCloud("�� �� ������ ���-�����, ������ ������� �� ������"); break;
                        case 6: changeTextInCloud("� �� ������ �� ������ ������������ ��� ������"); break;
                        case 7: changeTextInCloud("�� ������ �������� �������� � ��������"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }
                
            }
            else if (step == 3 && click)
            {
                if (save.lang == "en")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("You can jump from enemies"); break;
                        case 2: changeTextInCloud("Sometime it is not necessary"); break;
                        case 3: changeTextInCloud("Then you can remove it with a dash"); break;
                        case 4: changeTextInCloud("Get to the button with dash and press it with shuriken"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }else if(save.lang == "ru")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("�� ������ ������� �� ������"); break;
                        case 2: changeTextInCloud("������ ��� �� �����"); break;
                        case 3: changeTextInCloud("����� �� ������ ������������ �����"); break;
                        case 4: changeTextInCloud("�������� ������� �� ������ � ����� ������ ���������"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }
                    
            }
            else if (step == 4 && click)
            {
                if (save.lang == "en")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("You can jump a long distance"); break;
                        case 2: changeTextInCloud("If the accelerate"); break;
                        case 3: changeTextInCloud("Double jump completely extingushes inertia"); break;
                        case 4: changeTextInCloud("And jump on enemy too"); break;
                        case 5: changeTextInCloud("Beter step back and have a good speed"); break;
                        case 6: changeTextInCloud("And use the double jump at the end"); break;
                        case 7: changeTextInCloud("If you do not reacha little"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }else if (save.lang == "ru")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("� ���� ���� ����������� ������� �� ������� ���������"); break;
                        case 2: changeTextInCloud("���� �����������"); break;
                        case 3: changeTextInCloud("������� ������ ��������� ����� �������"); break;
                        case 4: changeTextInCloud("� ������ �� ����� ����"); break;
                        case 5: changeTextInCloud("����� ������ ����� � ������ �������� �����������"); break;
                        case 6: changeTextInCloud("��������� ������� ������ � �����"); break;
                        case 7: changeTextInCloud("���� �� �� ��������� �������"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }
            }
            else if (step == 5 && click)
            {
                if (save.lang == "en")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("I don't think int's necessary to tell you how to use spring"); break;
                        case 2: changeTextInCloud("It will send you just on the spikes"); break;
                        case 3: changeTextInCloud("Just do a week double jump at the right moment"); break;
                        case 4: changeTextInCloud("(click and don't move joystick)"); break;
                        case 5: changeTextInCloud("I'll be waiting at the end"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }else if(save.lang == "ru")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("� ����� �� ����� �������� ��� �������� �������"); break;
                        case 2: changeTextInCloud("��� ������� ���� �������� �� ����"); break;
                        case 3: changeTextInCloud("�� ��������, ������ ������ ������ ��� ����������� ���� ������� ������"); break;
                        case 4: changeTextInCloud("(������ �� �������� � �� ������ ���)"); break;
                        case 5: changeTextInCloud("� ���� ����� � �����"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }
                    
            }
            else if (step == 6 && click)
            {
                if (save.lang == "en")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("This room is my shop"); break;
                        case 2: changeTextInCloud("It will have everything improved for you"); break;
                        case 3: changeTextInCloud("It seems that i told you everything you need"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }else if (save.lang == "ru")
                {
                    switch (replicNum)
                    {
                        case 1: changeTextInCloud("�� ����� �������� ��� ����������"); break;
                        case 2: changeTextInCloud("��� ����� ��� ���������"); break;
                        case 3: changeTextInCloud("�����, � ��������� ��� ��� �����"); break;
                        default: enebleControl(true); showText(false); break;
                    }
                }
                    
            }
        }

        if(step == 1 && replicNum == 13)
        {
            workFunkJump = true;
            if(oneResetTimer)
            {
                timer = resetTimer;
                oneResetTimer = false;    
            }
            if(workFunkJump) workFunkJump = ShowArea(jumpArea);
        }else if(step == 2 && replicNum == 8)
        {
            workFunkJump = false;
            workFunkThrow = true;
            if (jumpArea != null)
            {
                Destroy(jumpArea);
            }
            if (oneResetTimer2)
            {
                timer = resetTimer;
                oneResetTimer2 = false;
            }
            if (workFunkThrow) workFunkThrow = ShowArea(throwArea);
        }
        else if (step == 3 && replicNum == 5)
        {
            workFunkThrow = false;
            workFunkDash = true;
            if (jumpArea != null)
            {
                Destroy(throwArea);
            }
            if (oneResetTimer3)
            {
                timer = resetTimer;
                oneResetTimer3 = false;
            }
            if (workFunkDash) workFunkDash = ShowArea(dashArea);
        }

        stoppingSpeakAnim();
    }

    public  void nextStep()
    {
        step++;
        replicNum = 1;
        enebleControl(false);
    }

    private void changeTextInCloud(string str)
    {
        showText(true);
        text.text = str;
        playSpeak();
        click = false;
    }

    private void playSpeak()
    {
        speakAnim.Play("SpeakGhost");
    }

    private void stoppingSpeakAnim()
    {
        if(spriteSpeak.enabled == false)
        {
            speakAnim.Play("speakIdle");
        }
    }

    private bool controlEnabled;
    private void enebleControl(bool state)
    {
        jumpJoy.SetActive(state);
        dashJoy.SetActive(state);
        throwJoy.SetActive(state);
        controlEnabled = state;
    }


    public void showText(bool state)
    {
        if(state == false)
        {
            cloudText.enabled = false;
            text.text = "";
        }
        else
        {
            cloudText.enabled = true;
        }
    }

    private float timer = 6;
    private float resetTimer = 6;
    private bool workFunkJump = false;
    private bool workFunkThrow = false;
    private bool workFunkDash = false;
    private bool oneResetTimer;
    private bool oneResetTimer2;
    private bool oneResetTimer3;
    private bool ShowArea(SpriteRenderer area)
    {
        
        if (timer > 0)
        {
            
            if (area != null)
            {
                area.gameObject.SetActive(true);
                area.color = Color.LerpUnclamped(new Color(0.9f, 0.9f, 0.9f, 1), new Color(0.9f, 0.9f, 0.9f, 0), Mathf.Abs(Mathf.Sin(Time.time)));
            }
               
            timer -= Time.deltaTime;
            return true;
        }
        else
        {
            timer = resetTimer;
            Destroy(area.gameObject);
            return false;
        }
    }
}
