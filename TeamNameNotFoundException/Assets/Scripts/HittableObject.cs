using UnityEngine;
using System.Collections;

public abstract class HittableObject : MonoBehaviour, IHittable {

	public abstract void Damaged(int damage);
}
