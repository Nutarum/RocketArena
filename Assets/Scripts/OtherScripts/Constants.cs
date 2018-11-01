
public class Constants{

    public static int ROUND_LIMIT = 9;

    public static int OBSTACLE_HP = 50;
    public static float OBSTACLE_EXPLOSION_AOE = 6;
    public static float OBSTACLE_EXPLOSION_DAMAGE = 10;

    public static int STARTING_SKILLPOINTS = 9;
    public static int SKILLPOINTS_PER_ROUND = 4;
    public static int INCREASE_SKILL_POINT_EARNED_EVERY_X_ROUNDS = 2;

    public static int SKILLPOINTS_COST_NEW_SPELL = 5;
    public static int SKILLPOINTS_COST_BASIC_UPGRADE = 1;
    public static int SKILLPOINTS_COST_ULTIMATE_UPGRADE = 2;

    //cuando recuperar el control de moverte despues de ser empujado
    public static float ALLOW_MOVEMENT_SPEED = 4;

    public static float BASIC_DRAG = 30f;
    public static float IN_RUSH_DRAG = 15f; //this is added to basic drag
    public static float IN_METAL_DRAG = 40f;//this is added to basic drag

    public static float STARTING_MAP_RADIUS = 30;

    public static float SHOP_TIME = 30f;

    public static float PLAYER_BASE_SPEED = 5f;

    public static float FIREBALL_CASTING_TIME = 0.4f;
    public static float FIREBALL_BASIC_VELOCITY = 33f;
    public static float FIREBALL_BASIC_DAMAGE = 8;
    public static float FIREBALL_CD = 4;
    public static float FIREBALL_BASIC_RANGE = 0.8f; //range = life time

    public static float BASICAOE_RADIUS = 6;
    public static float BASICAOE_DAMAGE = 10;
    public static float BASICAOE_CASTING_TIME = 1f;
    public static float BASICAOE_CD = 6f;

    public static float TELEPORT_RANGE = 14;
    public static float TELEPORT_CD = 8;

    public static float RUSH_CD = 8;
    public static float RUSH_DAMAGE = 10;
    public static float RUSH_FORCE = 30; //OJO CON AUNMENTARLO QUE SE PUEDEN JODER LAS COLISIONES
    public static float RUSH_DURATION = 3;
    

    public static float REFLECTOR_CD = 14;
    public static float REFLECTOR_DURATION = 1;

    public static float METALLIZE_CD = 14;
    public static float METALLIZE_DURATION = 2.5f;

    public static float ELECTRIC_TRAP_CD = 12;
    public static float ELECTRIC_TRAP_DAMAGE = 6;
    public static float ELECTRIC_TRAP_DURATION = 2;
    public static float ELECTRIC_TRAP_CAST_TIME = 2;

    public static float MISSILE_BARRAGE_CASTING_TIME = 0.5f;
    public static float MISSILE_BARRAGE_BASIC_RANGE_MOD = 0.4f;
    public static float MISSILE_BARRAGE_CD = 15;
    public static float MISSILE_BARRAGE_DAMAGE = 0.6f;
    public static float MISSILE_BARRAGE_SIZE_MOD = 0.7f;

    public static float TIME_BEACON_CD = 14;
    public static float TIME_BEACON_DURATION = 3;
    public static float TIME_BEACON_SPEED_MOD = 1.3f;
    public static float TIME_BEACON_SPEED_DURATION = 3;
    public static float TIME_BEACON_THROW_RANGE = 10;


    public static float CONFUSION_CASTING_TIME = 0.5f;
    public static float CONFUSION_CASTING_CD = 18;
    public static float CONFUSIONSHOT_BASIC_VELOCITY = 25f;
    public static float CONFUSIONSHOT_BASIC_DURATION = 4f;
    public static float CONFUSIONSHOT_RANDOM_TELEPORT_RANGE = 5f; 
    public static float CONFUSIONSHOT_BASIC_RANGE = 0.7f; //RANGE = LIFE TIME
    public static float CONFUSIONSHOT_BASIC_DAMAGE = 3f;
    public static float CONFUSIONSHOT_INCREASED_KNOCKBACK_MODIFIER = 30f;

    public static float STIMPAK_DURATION = 15f;
    public static int STIMPAK_STARTING_CHARGES = 2;
    public static float STIMPAK_SPEED_BOOST = 1.2f;
    public static float STIMPAK_HEALTH_REGEN = 30f;
}
