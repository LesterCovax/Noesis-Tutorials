// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using System;
using Float = System.Single;
#else
using System;
using System.Windows;
using System.Windows.Media.Animation;
using Float = System.Double;
#endif

namespace CustomAnimation
{
    /// <summary>
    ///     BounceAnimation
    /// </summary>
    public class BounceAnimation : DoubleAnimationBase
    {
        public enum EdgeBehaviorEnum
        {
            EaseIn,
            EaseOut,
            EaseInOut
        }

        public static readonly DependencyProperty EdgeBehaviorProperty = DependencyProperty.Register(
            "EdgeBehavior", typeof(EdgeBehaviorEnum), typeof(BounceAnimation),
            new PropertyMetadata(EdgeBehaviorEnum.EaseInOut));

        public static readonly DependencyProperty BouncesProperty = DependencyProperty.Register(
            "Bounces", typeof(int), typeof(BounceAnimation),
            new PropertyMetadata(5));

        public static readonly DependencyProperty BouncinessProperty = DependencyProperty.Register(
            "Bounciness", typeof(Float), typeof(BounceAnimation),
            new PropertyMetadata((Float)3.0));

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From", typeof(Float?), typeof(BounceAnimation),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To", typeof(Float?), typeof(BounceAnimation),
            new PropertyMetadata(null));

        /// <summary>
        ///     Specifies which side of the transition gets the "bounce" effect.
        /// </summary>
        public EdgeBehaviorEnum EdgeBehavior
        {
            get { return (EdgeBehaviorEnum) GetValue(EdgeBehaviorProperty); }
            set { SetValue(EdgeBehaviorProperty, value); }
        }

        /// <summary>
        ///     Number of bounces in the effect
        /// </summary>
        public int Bounces
        {
            get { return (int) GetValue(BouncesProperty); }
            set
            {
                if (value > 0)
                {
                    SetValue(BouncesProperty, value);
                }
                else
                {
                    throw new ArgumentException("can't set the bounces to " + value);
                }
            }
        }

        /// <summary>
        ///     Specifies the amount by which the element springs back.
        /// </summary>
        public Float Bounciness
        {
            get { return (Float) GetValue(BouncinessProperty); }
            set
            {
                if (value > 0)
                {
                    SetValue(BouncinessProperty, value);
                }
                else
                {
                    throw new ArgumentException("can't set the bounciness to " + value);
                }
            }
        }

        /// <summary>
        ///     Specifies the starting value of the animation.
        /// </summary>
        public Float? From
        {
            get { return (Float?) GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        ///     Specifies the ending value of the animation.
        /// </summary>
        public Float? To
        {
            get { return (Float?) GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        protected override Float GetCurrentValueCore(
            Float defaultOriginValue,
            Float defaultDestinationValue,
            AnimationClock clock)
        {
            Float returnValue;
            var start = From ?? defaultOriginValue;
            var delta = To - start ?? defaultOriginValue - start;

            switch (EdgeBehavior)
            {
                case EdgeBehaviorEnum.EaseIn:
                    returnValue = EaseIn(clock.CurrentProgress.Value, start, delta, Bounciness, Bounces);
                    break;
                case EdgeBehaviorEnum.EaseOut:
                    returnValue = EaseOut(clock.CurrentProgress.Value, start, delta, Bounciness, Bounces);
                    break;
                default:
                    returnValue = EaseInOut(clock.CurrentProgress.Value, start, delta, Bounciness, Bounces);
                    break;
            }
            return returnValue;
        }

#if !NOESIS
        protected override Freezable CreateInstanceCore() => new BounceAnimation();
#endif

        private static Float EaseOut(Float timeFraction, Float start, Float delta, Float bounciness, int bounces)
        {
            // math magic: The cosine gives us the right wave, the timeFraction is the frequency of the wave, 
            // the absolute value keeps every value positive (so it "bounces" off the midpoint of the cosine 
            // wave, and the amplitude (the exponent) makes the sine wave get smaller and smaller at the end.
            var returnValue = Math.Abs(Math.Pow((1 - timeFraction), bounciness)
                                       *Math.Cos(2*Math.PI*timeFraction*bounces));
            returnValue = delta - (returnValue*delta);
            returnValue += start;
            return (Float)returnValue;
        }

        private static Float EaseIn(Float timeFraction, Float start, Float delta, Float bounciness, int bounces)
        {
            // math magic: The cosine gives us the right wave, the timeFraction is the amplitude of the wave, 
            // the absolute value keeps every value positive (so it "bounces" off the midpoint of the cosine 
            // wave, and the amplitude (the exponent) makes the sine wave get bigger and bigger towards the end.
            var returnValue = Math.Abs(Math.Pow((timeFraction), bounciness)
                                       *Math.Cos(2*Math.PI*timeFraction*bounces));
            returnValue = returnValue*delta;
            returnValue += start;
            return (Float)returnValue;
        }

        private static Float EaseInOut(Float timeFraction, Float start, Float delta, Float bounciness, int bounces)
        {
            Float returnValue;

            // we cut each effect in half by multiplying the time fraction by two and halving the distance.
            if (timeFraction <= 0.5)
            {
                returnValue = EaseIn(timeFraction*2, start, delta/2, bounciness, bounces);
            }
            else
            {
                returnValue = EaseOut((timeFraction - 0.5f)*2, start, delta/2, bounciness, bounces);
                returnValue += delta/2;
            }
            return returnValue;
        }
    }
}