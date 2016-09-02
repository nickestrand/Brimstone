using System;
using System.Collections;
using System.Collections.Generic;

namespace Brimstone
{
	public class EntityController : IEnumerable<IEntity>, ICloneable
	{
		public Game Game { get; }
		public int NextEntityId = 1;
		private Dictionary<int, IEntity> Entities = new Dictionary<int, IEntity>();

		public IEntity this[int id]
		{
			get { return Entities[id]; }
		}

		public int Count
		{
			get { return Entities.Count; }
		}

		public ICollection<int> Keys
		{
			get { return Entities.Keys; }
		}

		public bool ContainsKey(int key)
		{
			return Entities.ContainsKey(key);
		}

		public EntityController(Game game)
		{
			Game = game;
			Game.ZoneController = game;
			Add(game);
		}

		public EntityController(EntityController es)
		{
			NextEntityId = es.NextEntityId;
			foreach (var entity in es)
				Entities.Add(entity.Id, (IEntity) entity.Clone());
			// Change ownership
			Game = FindGame();
			foreach (var entity in Entities)
				entity.Value.Game = Game;
			foreach (var entity in Entities)
				entity.Value.ZoneController = (IZoneController) Entities[es.Entities[entity.Key].ZoneController.Id];

			// Do this last so that changing Controller doesn't trigger EntityChanging
			Game.Entities = this;
		}

		public IEntity Add(IEntity entity)
		{
			entity.Game = Game;
			entity.Id = NextEntityId++;
			Entities[entity.Id] = entity;
			Game.EntityChanging(entity.Id, 0);
			if (Game.PowerHistory != null)
				Game.PowerHistory.Add(new CreateEntity(entity));
			Game.ActiveTriggers.Add(entity);
			return entity;
		}

		public Game FindGame()
		{
			// Game is always entity ID 1
			return (Game) Entities[1];
		}

		public Player FindPlayer(int p)
		{
			// Player is always p+1
			return (Player) Entities[p + 1];
		}

		public IEnumerator<IEntity> GetEnumerator()
		{
			return Entities.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public object Clone()
		{
			return new EntityController(this);
		}
	}
}
