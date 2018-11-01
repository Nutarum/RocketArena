using UnityEngine;

public class Utils {

    public static Vector3 getBounce(Vector3 position, Vector3 velocity, Collider collider) {
        RaycastHit hit;
        Ray ray = new Ray(position, (collider.ClosestPointOnBounds(position) - position).normalized);

        //a veces, cuando el bicho se movia muy rapido, la direccion del ray se quedaba en 0
        //solicion: cuando es 0, el ray empieza desde mas atras del bicho
        int cont = 1;
        while (ray.direction.magnitude == 0 && cont < 5) {
            ray = new Ray(position - velocity.normalized* cont, (collider.ClosestPointOnBounds(position) - (position - velocity.normalized* cont)).normalized);
            cont++;
        }
        
        Debug.Log(cont);

        if (collider.Raycast(ray, out hit, 1)) {
            Vector3 towards = Vector3.Reflect(velocity, hit.normal);
            return Vector3.RotateTowards(velocity, towards, 100, 0.0f);
        }
        return velocity;
    }
}
