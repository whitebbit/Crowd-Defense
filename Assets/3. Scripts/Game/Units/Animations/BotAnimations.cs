﻿using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Units.Scriptable.Animations;
using FSG.MeshAnimator;
using FSG.MeshAnimator.Snapshot;
using JetBrains.Annotations;
using UnityEngine;

namespace _3._Scripts.Game.Units.Animations
{
    public class BotAnimations: IAnimator
    {
        private readonly MeshAnimatorBase _meshAnimator;
        private readonly MeshAnimationsHolder _animations;
        
        public BotAnimations(MeshAnimatorBase meshAnimator, MeshAnimationsHolder animations)
        {
            _meshAnimator = meshAnimator;
            _animations = animations;
        }
        
        public void State(bool state)
        {
            if(state)
                _meshAnimator.Play();
            else
                _meshAnimator.Pause();
        }

        public void Play(string key)
        {
            _meshAnimator.Play(_animations.Get(key).animationName);
        }

        public void PlayRandom(string key)
        {
            var list = _animations.GetList(key);
            var names = list.Select(a => a.animationName).ToArray();
            
            _meshAnimator.PlayRandom(names);
        }
    }
}