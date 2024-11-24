using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Player class
    public class Player
    {
        public int hp = 100;
        public Inventory inventory = new Inventory(6);
        public Weapon pistol = new Weapon("Pistol", 30, 0, 100); // 30 damage, unlimited range
        public Weapon shotgun = new Weapon("Shotgun", 0, 3, 10); // Damage varies by range

        // Medkit heals 40 HP
        public void UseMedkit()
        {
            if (inventory.Contains("Medkit"))
            {
                hp = Mathf.Min(hp + 40, 100); // Heal up to max HP
                inventory.RemoveItem("Medkit");
                Debug.Log("Medkit used. Current HP: " + hp);
            }
            else
            {
                Debug.Log("No medkit available.");
            }
        }

        // Attack logic based on weapon and range
        public void Attack(Weapon weapon, Zombie zombie, float range)
        {
            if (weapon.name == "Shotgun")
            {
                int damage = weapon.CalculateShotgunDamage(range);
                zombie.TakeDamage(damage);
                Debug.Log($"Shotgun hit! Dealt {damage} damage to zombie.");
            }
            else if (weapon.name == "Pistol")
            {
                zombie.TakeDamage(weapon.damage);
                Debug.Log("Pistol hit! Dealt 30 damage to zombie.");
            }
        }
    }

    // Zombie class
    public class Zombie
    {
        public int hp;
        public bool isAggro = false;

        public Zombie()
        {
            hp = Random.Range(100, 151); // Initialize between 100 and 150 HP
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Debug.Log("Zombie killed.");
            }
            else
            {
                Debug.Log($"Zombie HP: {hp}");
            }
        }

        // Grab and Bite attack
        public void GrabAndBite(Player player, string difficulty)
        {
            int biteDamage = difficulty == "Hard" ? 50 : 25;
            player.hp -= biteDamage;
            Debug.Log($"Zombie bit the player for {biteDamage} damage. Player HP: {player.hp}");
        }

        // Aggro on spotting the player
        public void SpotPlayer()
        {
            isAggro = true;
            Debug.Log("Zombie spotted the player! Aggro activated.");
        }
    }

    // Weapon class
    public class Weapon
    {
        public string name;
        public int damage;
        public float effectiveRange;
        public float maxRange;

        public Weapon(string name, int damage, float effectiveRange, float maxRange)
        {
            this.name = name;
            this.damage = damage;
            this.effectiveRange = effectiveRange;
            this.maxRange = maxRange;
        }

        // Shotgun damage calculation
        public int CalculateShotgunDamage(float range)
        {
            if (range <= 3)
            {
                return 100; // Instakill within 3 meters
            }
            else if (range > 3 && range <= 10)
            {
                return 37; // 37 damage between 3 and 10 meters
            }
            else
            {
                return 20; // 20 damage beyond 10 meters
            }
        }
    }

    // Inventory class
    public class Inventory
    {
        private int capacity;
        private List<string> items;

        public Inventory(int capacity)
        {
            this.capacity = capacity;
            this.items = new List<string>();
        }

        public bool AddItem(string item)
        {
            if (items.Count < capacity)
            {
                items.Add(item);
                Debug.Log($"Added {item} to inventory.");
                return true;
            }
            else
            {
                Debug.Log("Inventory full.");
                return false;
            }
        }

        public void RemoveItem(string item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                Debug.Log($"Removed {item} from inventory.");
            }
            else
            {
                Debug.Log($"{item} not in inventory.");
            }
        }

        public bool Contains(string item)
        {
            return items.Contains(item);
        }

        public void DisplayInventory()
        {
            Debug.Log("Inventory: " + string.Join(", ", items));
        }
    }

    // Example of gameplay logic
    void Start()
    {
        // Initialize player and zombie
        Player player = new Player();
        Zombie zombie = new Zombie();

        // Player picks up items
        player.inventory.AddItem("Medkit");
        player.inventory.AddItem("Shotgun Ammo");

        // Zombie spots player and aggros
        zombie.SpotPlayer();

        // Player attacks zombie
        player.Attack(player.shotgun, zombie, 5f); // Shotgun hit at 5 meters
        player.Attack(player.pistol, zombie, 0f);  // Pistol hit

        // Zombie grabs and bites player
        zombie.GrabAndBite(player, "Normal");

        // Player uses a medkit
        player.UseMedkit();

        // Display remaining inventory
        player.inventory.DisplayInventory();
    }
}
