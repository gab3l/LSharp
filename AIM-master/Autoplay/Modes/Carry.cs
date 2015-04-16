using System;
using AIM.Autoplay.Util.Helpers;
using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace AIM.Autoplay.Modes
{
    internal class Carry : Base
    {
        public Carry()
        {
            Game.OnUpdate += OnGameUpdate;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }

        public override void OnGameLoad(EventArgs args)
        {
            FileHandler.DoChecks();
        }

        public override void OnGameUpdate(EventArgs args)
        {
            ObjHeroes.SortHeroesListByDistance();
            ObjTurrets.UpdateTurrets();
			
            ImpingAintEasy();
            RefreshMinions();
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender == null || !sender.IsValid || args == null)
            {
                return;
            }

            var target = args.Target;

            if (target == null || !target.IsValid)
            {
                return;
            }

            if (sender.IsMe && sender.UnderTurret(true) && target.IsEnemy)
            {

            }

            if (sender is Obj_AI_Turret && target.IsMe)
            {
                
            }

            if (sender is Obj_AI_Minion && target.IsMe)
            {
                var orbwalkingPos = new Vector2
                {
                    X = ObjectManager.Player.Position.X + ObjConstants.DefensiveAdditioner,
                    Y = ObjectManager.Player.Position.Y + ObjConstants.DefensiveAdditioner
                };
                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, orbwalkingPos.To3D());
            }
        }

        public void ImpingAintEasy()
        {
            MetaHandler.DoChecks(); //#TODO rewrite MetaHandler with BehaviorSharp

            Behaviors.MainBehavior.Root.Tick();
        }
    }
}