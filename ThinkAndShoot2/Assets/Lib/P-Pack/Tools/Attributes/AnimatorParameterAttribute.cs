using UnityEngine;

public sealed class AnimatorParameterAttribute : AnimatorBaseAttribute
{
    public bool anyType = false;

    public AnimatorParameterAttribute() : base()
    {
        anyType = true;
    }

    public AnimatorParameterAttribute(AnimatorControllerParameterType type) : base()
	{
		m_type = type;
	}

	public AnimatorParameterAttribute(string animatorPropertyName, AnimatorControllerParameterType type)
		: base(animatorPropertyName)
	{
		m_type = type;
	}

	private AnimatorControllerParameterType m_type;
	public AnimatorControllerParameterType type { get { return m_type; } }
}
