using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabViewHelper : MonoBehaviour
{
    public VisualTreeAsset view;
    private VisualElement viewRoot;
    private UIDocument uIDocument;
    private Button button1;
    private Button button2;
    private Button button3;
    private GroupBox content1;
    private GroupBox content2;
    private GroupBox content3;
    private ScrollView scrollView;
    string[] animals;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        uIDocument.visualTreeAsset = view;
        viewRoot = uIDocument.rootVisualElement;
        viewRoot.visible = true;
        AssignButtons();
        AssignContents();
        SetContentVisible(content1);
        LoadAnimals();
        Button1Clicked();
    }

    private void AssignContents()
    {
        content1 = viewRoot.Q<GroupBox>("content1");
        content2 = viewRoot.Q<GroupBox>("content2");
        content3 = viewRoot.Q<GroupBox>("content3");
        scrollView = viewRoot.Q<ScrollView>("scrollView");
    }

    private void AssignButtons()
    {
        button1 = viewRoot.Q<Button>("button1");
        button2 = viewRoot.Q<Button>("button2");
        button3 = viewRoot.Q<Button>("button3");
        button1.clicked += Button1Clicked;
        button2.clicked += Button2Clicked;
        button3.clicked += Button3Clicked;

    }

    private void SetContentVisible(GroupBox content)
    {
        switch (content.name)
        {
            case "content1":
                content.style.display = DisplayStyle.Flex;
                content2.style.display = DisplayStyle.None;
                content3.style.display = DisplayStyle.None;
                break;
            case "content2":
                content.style.display = DisplayStyle.Flex;
                content1.style.display = DisplayStyle.None;
                content3.style.display = DisplayStyle.None;
                break;
            case "content3":
                content.style.display = DisplayStyle.Flex;
                content1.style.display = DisplayStyle.None;
                content2.style.display = DisplayStyle.None;
                break;
            default:
                break;
        }
    }

    private void Button1Clicked()
    {
        button1.AddToClassList("activeTabButton");
        button2.RemoveFromClassList("activeTabButton");
        button3.RemoveFromClassList("activeTabButton");
        SetContentVisible(content1);
        if (content1.childCount == 0)
        {
            for (int i = 0; i < 300; i++)
            {
                Label newLabel = new(GetRandomAnimal());
                newLabel.AddToClassList("scrollViewLabel");
                scrollView.Add(newLabel);
            }
            if (SystemInfo.deviceType == DeviceType.Handheld)
                scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        }
    }

    private void Button2Clicked()
    {
        button2.AddToClassList("activeTabButton");
        button1.RemoveFromClassList("activeTabButton");
        button3.RemoveFromClassList("activeTabButton");
        SetContentVisible(content2);
    }

    private void Button3Clicked()
    {
        button3.AddToClassList("activeTabButton");
        button1.RemoveFromClassList("activeTabButton");
        button2.RemoveFromClassList("activeTabButton");
        SetContentVisible(content3);
        if (content3.childCount == 0)
        {
            Image meme = new()
            {
                image = Resources.Load<Texture>("NiceMeme")
            };

            meme.style.width = new Length(100,LengthUnit.Percent);
            meme.style.height = new Length(100, LengthUnit.Percent);
            content3.Add(meme);
        }
    }

    private void LoadAnimals()
    {
        TextAsset animalsText = Resources.Load<TextAsset>("animals");
        string convertedLines = animalsText.text.Replace("\n", "\r\n");
        animals = convertedLines.Split(Environment.NewLine);
    }

    private string GetRandomAnimal()
    {
        return animals[UnityEngine.Random.Range(0, animals.Length)];
    }
}
