using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    [Header("SkullStuff")]
    public string[] normalAnimations;
    public string[] enthusiasticAnimations;
    public Animator anim;
    public float animationDelay;
    public AudioClip[] sounds;
    public AudioSource source;
    public Vector2 normalSoundRange, enthusiasticSoundRange;
    public bool following;
    public float returnDelay;
    public Transform pedastalPos;

    [Header("SkullHover")]
    public float speed;
    public float heightOffset;
    public float normalHeight;

    [Header("SkullRotation")]
    public float lerpSpeed;
    public float lookLerp;
    public float rotateSpeed;
    public float lookHeightOffset;
    public Transform followObject, player, skull;
    public Vector3 lookPos;

    [Header("Other")]
    public Text textInput;
    public GameObject uiPanel;
    public UnityEvent[] events;
    public bool active;
    public Saving saving;
    public string savingTag;
    public DialogueInfo tutorialDialogue;
    public bool inShop;

    public void Start()
    {
        saving = GameObject.FindWithTag(savingTag).GetComponent<Saving>();
        normalHeight = followObject.position.y;
        if (!saving.data.hadTutorialShop && inShop || !saving.data.hadTutorialDungeon && !inShop)
            StartCoroutine(StartDialogue(tutorialDialogue));
        StartCoroutine(Hover());
    }

    public void Update()
    {
        if(following)
            Follow();
    }

    public IEnumerator Hover()
    {
        int invert = 1;
        while (true)
        {
            yield return null;
            while (Mathf.Abs(followObject.position.y - (normalHeight + (heightOffset * invert))) > 0.01f)
            {
                if(following)
                    followObject.position = Vector3.Lerp(followObject.position, new Vector3(followObject.position.x, normalHeight + (heightOffset * invert), followObject.position.z), Time.deltaTime * speed);
                yield return null;
            }
            invert = -invert;
        }
    }

    public void Follow()
    {
        followObject.RotateAround(player.position, Vector3.up, rotateSpeed * Time.deltaTime);
        skull.transform.position = Vector3.Lerp(skull.transform.position, followObject.transform.position, Time.deltaTime * lerpSpeed);

        var targetRotation = Quaternion.LookRotation(new Vector3(player.position.x, player.position.y + lookHeightOffset, player.position.z) - skull.position);
        skull.rotation = Quaternion.Lerp(skull.rotation, targetRotation, Time.deltaTime * lookLerp);
    }

    public IEnumerator StartDialogue(DialogueInfo info)
    {
        uiPanel.SetActive(true);
        active = true;
        foreach (DialoguePartInfo dialoguePart in info.dialogue)
        {
            yield return null;
            foreach (int eventIndex in dialoguePart.beginActionEvents)
                events[eventIndex].Invoke();

            StartCoroutine(PlayAnimations(dialoguePart.animationAmount, dialoguePart.enthusiastic, dialoguePart.soundAmount));
            textInput.text = "";
            foreach (char letter in dialoguePart.message)
            {
                yield return null;
                textInput.text += letter;
                if (Input.GetButtonDown("Fire1"))
                    break;
            }
            textInput.text = dialoguePart.message;
            yield return null;

            while (!Input.GetButtonDown("Fire1"))
                yield return null;

            foreach (int eventIndex in dialoguePart.endActionEvents)
                events[eventIndex].Invoke();
        }
        if (!active)
            uiPanel.SetActive(false);
    }

    public IEnumerator PlayAnimations(int amount, bool enthusiastic, int soundClips)
    {
        StartCoroutine(PlaySounds(soundClips, enthusiastic));
        for (int i = 0; i < amount; i++)
        {
            int animation = Random.Range(0, enthusiastic ? enthusiasticAnimations.Length : normalAnimations.Length);
            anim.SetTrigger(enthusiastic ? enthusiasticAnimations[animation] : normalAnimations[animation]);
            yield return new WaitForSeconds(animationDelay);
        }
    }

    public IEnumerator PlaySounds(int amount, bool enthusiastic)
    {
        for (int i = 0; i < amount; i++)
        {
            source.pitch = enthusiastic ? Random.Range(enthusiasticSoundRange.x, enthusiasticSoundRange.y) : Random.Range(normalSoundRange.x, normalSoundRange.y);
            int index = Random.Range(0, sounds.Length);
            source.PlayOneShot(sounds[index]);
            yield return new WaitForSeconds(sounds[index].length);
        }
    }

    public IEnumerator SendSkullToPedastal()
    {
        anim.SetTrigger("Spin");
        following = false;
        yield return new WaitForSeconds(returnDelay);
        skull.transform.position = pedastalPos.position;
        skull.transform.rotation = pedastalPos.rotation;
    }
}
