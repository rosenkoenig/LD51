using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsstractButtonMouseInteractible : AbstractMouseInteractible
{
    public bool Hovered { get { return hovered; } }
    bool hovered = false;

    public bool Pressed { get { return pressed; } }
    bool pressed = false;

    protected override void UpdateMouseInteractible()
    {
        if (PointAndClickInputs.Instance == null)
        {
            Debug.LogError("Point And Click input instance is null");
        }

        bool isTouched = PointAndClickInputs.Instance.GetGameObjectIsTouched(gameObject);

        IsHovered(isTouched);

        if (isTouched)
        {
            if (Input.GetMouseButtonDown(0))
            { OnPress(); }
        }

        if (pressed && Input.GetMouseButtonUp(0))
        { internal_OnRelease(); }
    }

    void IsHovered(bool state)
    {
        if (Input.GetMouseButton(0)) return;

        if (hovered == false && state == true) OnMouseIn();
        if (hovered == true && state == false) OnMouseOut();

        hovered = state;
    }

    protected virtual void OnMouseIn()
    {

    }

    protected virtual void OnMouseOut()
    {

    }

    protected virtual void OnPress()
    {
        pressed = true;
    }

    protected void internal_OnRelease()
    {
        if (!hovered)
        {
            pressed = false;
            return;
        }
        else
        {
            OnRelease();
            if (pressed) OnClick();
        }

        pressed = false;
    }
    protected virtual void OnRelease()
    {

    }

    protected virtual void OnClick()
    {

    }
}
