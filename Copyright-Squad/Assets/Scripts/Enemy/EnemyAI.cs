using UnityEngine;
using System.Collections;

namespace Pathfinding
{

	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class EnemyAI : VersionedMonoBehaviour
	{
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;
		public LayerMask obstacleLayer;

		private Transform player;
		private SpriteRenderer spr;
		void OnEnable()
		{
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}
        private void Start()
        {
			target = GameObject.FindGameObjectWithTag("Player").transform;
			player = GameObject.FindGameObjectWithTag("Player").transform;
			spr = GetComponent<SpriteRenderer>();
		}

		void OnDisable()
		{
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update()
		{
			if (target != null && ai != null) ai.destination = target.position;

			RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, obstacleLayer);
			if (hit.collider != null)
            {
				target = player;
            }
			else
            {
				target = null;
			}
			Flip();
		}
		private void Flip()
        {
			if (target != null)
            {
				Vector2 direction = (target.position - transform.position).normalized;
				spr.flipX = direction.x < 0;
				if (direction.x > 0)
				{
					this.transform.GetChild(0).transform.position = new Vector2(transform.position.x + 0.18f, transform.position.y - 0.54f);
				}
				else if (direction.x < 0)
				{
					this.transform.GetChild(0).transform.position = new Vector2(transform.position.x - 0.18f, transform.position.y - 0.54f);

				}
				else
					return;
			}

		}
	}
}
