ARCHIVO_INVENTARIO = "inventario_pipe.txt"

def registrar_producto():
    codigo = input("Ingrese codigo del producto (Ej: P10): ").strip().upper()
    nombre = input("Ingrese nombre o descripcion: ").strip()
    precio = float(input("Ingrese precio en soles (Ej: 4.50): ").strip())
    stock = int(input("Ingrese stock disponible: ").strip())

    registro = f"{codigo}|{nombre}|{precio:.2f}|{stock}\n"
    archivo = open(ARCHIVO_INVENTARIO, "a", encoding="utf-8")
    archivo.write(registro)
    archivo.close()
    print(">> Producto registrado con exito con formato PIPE.")


def buscar_por_codigo():
    codigo_buscado = input("Ingrese codigo del producto a buscar: ").strip().upper()
    archivo = open(ARCHIVO_INVENTARIO, "r", encoding="utf-8")
    encontrado = False

    for linea in archivo:
        campos = [campo.strip() for campo in linea.split("|")]
        if len(campos) == 4 and campos[0].upper() == codigo_buscado:
            print(f"\n>> Producto encontrado: {campos[1]}")
            print(f"   Precio: S/ {campos[2]}")
            print(f"   Stock disponible: {campos[3]} unidades")
            encontrado = True
            break

    archivo.close()

    if encontrado==False:
        print("\nProducto no encontrado en el inventario.")


def listar():
    archivo = open(ARCHIVO_INVENTARIO, "r", encoding="utf-8")

    for linea in archivo:
        campos = linea.split("|")
        codigo = campos[0]
        nombre = campos[1]
        precio = float(campos[2])   
        stock = int(campos[3])     
        print(f" [{codigo}] {nombre} - Stock actual: {stock} - precio {precio:.2f} ");
    archivo.close()

def modificar_producto():
    codigo_buscado = input("Ingrese código del producto a modificar: ").strip().upper()

    # 1. Cargamos todas las líneas a memoria
    archivo = open(ARCHIVO_INVENTARIO, "r", encoding="utf-8")
    lineas = archivo.readlines()
    archivo.close()

    lista_actualizada = []
    for linea in lineas:
        campos = linea.strip().split("|")
        if len(campos) == 4 and campos[0].upper() == codigo_buscado:
            print(f"Producto actual: {campos[1]} (Precio actual: S/ {campos[2]}, Stock actual: {campos[3]})")
            nuevo_precio = float(input("Ingrese NUEVO precio en soles: ").strip())
            nuevo_stock = int(input("Ingrese NUEVO stock: ").strip())
            lista_actualizada.append(f"{campos[0]}|{campos[1]}|{nuevo_precio:.2f}|{nuevo_stock}\n")
        else:
            lista_actualizada.append(linea)  # Conservamos sin cambios

    # 2. Reescribimos el archivo completo con los datos actualizados
    archivo = open(ARCHIVO_INVENTARIO, "w", encoding="utf-8")
    archivo.writelines(lista_actualizada)
    archivo.close()
    print("3Producto actualizado c3orrectamente en disco!")



def eliminar_producto():
    print("\n--- ELIMINAR PRODUCTO DEL INVENTARIO ---")
    codigo_eliminar = input("Ingrese código del producto a eliminar: ").strip().upper()

    # 1. Cargamos todas las líneas a memoria
    archivo = open(ARCHIVO_INVENTARIO, "r", encoding="utf-8")
    lineas = archivo.readlines()
    archivo.close()

    lista_filtrada = []
    for linea in lineas:
        campos = linea.strip().split("|")
        # Si NO es el código que queremos borrar, lo conservamos
        if len(campos) != 4 or campos[0].upper() != codigo_eliminar:
            lista_filtrada.append(linea)
        else:
            print(f"Eliminando el producto con código {codigo_eliminar}: {linea}")

    # 2. Reescribimos el archivo sin el registro eliminado
    archivo = open(ARCHIVO_INVENTARIO, "w", encoding="utf-8")
    archivo.writelines(lista_filtrada)
    archivo.close()


def main():
    opcion = 0

    while opcion != 6:
        print("\n=== MINIMARKET - INVENTARIO CON PIPE ===")
        print("1. Registrar producto")
        print("2. Buscar por codigo")
        print("3. Listar")
        print("4. Modificar")
        print("5. Eliminar")
        print("6. Salir")
        opcion = int(input("Opcion: ").strip())

        if opcion == 1:
            registrar_producto()
        elif opcion == 2:
            buscar_por_codigo()
        elif opcion == 3:
            listar()
        elif opcion == 4:
            modificar_producto()
        elif opcion == 5:
            eliminar_producto()
        elif opcion == 6:
            print("Saliendo del inventario...")
        else:
            print("Opcion no valida.")


main()