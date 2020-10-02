namespace RedBlueGames.Tools.TextTyper
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using RedBlueGames.Tools.TextTyper;
    using UnityEngine.UI;
    using TMPro;

    /// <summary>
    /// Class that tests TextTyper and shows how to interface with it.
    /// </summary>
    public class TextTyperTester : MonoBehaviour
    {
#pragma warning disable 0649 // Ignore "Field is never assigned to" warning, as these are assigned in inspector
        [SerializeField]
        private AudioClip printSoundEffect;

        [Header("UI References")]

        [SerializeField]
        private Button printNextButton;

        [SerializeField]
        private Button printNoSkipButton;

        [SerializeField]
        private Toggle pauseGameToggle;

        private Queue<string> dialogueLines = new Queue<string>();

        [SerializeField]
        [Tooltip("The text typer element to test typing with")]
        private TextTyper testTextTyper;

#pragma warning restore 0649
        public void Start()
        {
            this.testTextTyper.PrintCompleted.AddListener(this.HandlePrintCompleted);
            this.testTextTyper.CharacterPrinted.AddListener(this.HandleCharacterPrinted);

            this.printNextButton.onClick.AddListener(this.HandlePrintNextClicked);
            this.printNoSkipButton.onClick.AddListener(this.HandlePrintNoSkipClicked);

            dialogueLines.Enqueue("Hello! Lets Start our journey through the stars and learn about ... <delay=0.5><color=#ff0000ff>PROJECTILE MOTION</color></delay>. Got it?");
            dialogueLines.Enqueue("We use <b><color=#00ff00ff>PROJECTILE MOTION</color></b> to calculate the time it takes to reach a <size=40>target</size>.");
            dialogueLines.Enqueue("In the levels ahead ull see that the key factors of the formula are <b>Speed, Distance</b> and <b>Angle</b> ");
            dialogueLines.Enqueue("You can change the values by moving around");
            dialogueLines.Enqueue("Panel on the side will show the time being calculated in real time");
            dialogueLines.Enqueue("Just play around <color=#ff0000ff>Have fun</color> and find the <color=#00ff00ff>Target</color>.");
            dialogueLines.Enqueue("<color=#00ff00ff><anim=lightpos>BEST OF LUCK</anim></color>");
            ShowScript();
        }

        public void Update()
        {
            UnityEngine.Time.timeScale = this.pauseGameToggle.isOn ? 0.0f : 1.0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {

                var tag = RichTextTag.ParseNext("blah<color=red>boo</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("<color=blue>blue</color");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No tag in here");
                LogTag(tag);
                tag = RichTextTag.ParseNext("No <color=blueblue</color tag here either");
                LogTag(tag);
                tag = RichTextTag.ParseNext("This tag is a closing tag </bold>");
                LogTag(tag);
            }
        }

        private void HandlePrintNextClicked()
        {
            if (this.testTextTyper.IsSkippable() && this.testTextTyper.IsTyping)
            {
                this.testTextTyper.Skip();
            }
            else
            {
                ShowScript();
            }
        }

        private void HandlePrintNoSkipClicked()
        {
            ShowScript();
        }

        private void ShowScript()
        {
            if (dialogueLines.Count <= 0)
            {
                return;
            }

            this.testTextTyper.TypeText(dialogueLines.Dequeue());
        }

        private void LogTag(RichTextTag tag)
        {
            if (tag != null)
            {
                Debug.Log("Tag: " + tag.ToString());
            }
        }

        private void HandleCharacterPrinted(string printedCharacter)
        {
            // Do not play a sound for whitespace
            if (printedCharacter == " " || printedCharacter == "\n")
            {
                return;
            }

            var audioSource = this.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = this.gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = this.printSoundEffect;
            audioSource.Play();
        }

        private void HandlePrintCompleted()
        {
            Debug.Log("TypeText Complete");
        }
    }
}