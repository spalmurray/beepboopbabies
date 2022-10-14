using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    ///     It is an action to play an animation in the GameObject
    /// </summary>
    [Action("Animation/PlayAnimation")]
    [Help("Plays an animation in the game object")]
    public class PlayAnimation : GOAction
    {
        /// <summary>All Input Parameters of PlayAnimation action.</summary>
        /// <value>The clip that must be played.</value>
        [InParam("animationClip")] [Help("The clip that must be played")]
        public AnimationClip animationClip;

        ///<value>Wrapping mode of the animation.</value>
        [InParam("animationWrap")] [Help("Wrapping mode of the animation")]
        public WrapMode animationWrap = WrapMode.Loop;

        ///<value>Period of time to fade this animation in and fade other animations out.</value>
        [InParam("crossFadeTime", DefaultValue = 0.25f)]
        [Help("Period of time to fade this animation in and fade other animations out")]
        public float crossFadeTime = 0.25f;

        private float elapsedTime;

        ///<value>Wheter the action waits till the end of the animation to be completed.</value>
        [InParam("waitUntilFinish")] [Help("Wheter the action waits till the end of the animation to be completed")]
        public bool waitUntilFinish;

        /// <summary>Initialization Method of PlayAnimation.</summary>
        /// <remarks>Associate and Inacialize the animation and the elapsed time.</remarks>
        public override void OnStart()
        {
            var animation = gameObject.GetComponent<Animation>();
            animation.AddClip(animationClip, animationClip.name);

            animation[animationClip.name].wrapMode = animationWrap;
            animation.CrossFade(animationClip.name, crossFadeTime);

            elapsedTime = Time.time;
        }

        /// <summary>Method of Update of PlayAnimation.</summary>
        /// <remarks>Increase the elapsed time and check if the animation is over.</remarks>
        public override TaskStatus OnUpdate()
        {
            elapsedTime += Time.deltaTime;
            if (!waitUntilFinish || elapsedTime >= animationClip.length - crossFadeTime)
                return TaskStatus.COMPLETED;
            return TaskStatus.RUNNING;
        }
    }
}