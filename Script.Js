const apiUrl = "http://localhost:5000/api/ventas"; // URL de la API

async function obtenerVentas() {
    try {
        const response = await fetch(apiUrl);
        const ventas = await response.json();

        const tabla = document.getElementById('tabla-ventas');
        tabla.innerHTML = '';

        ventas.forEach(venta => {
            const row = `<tr>
                <td>${venta.folio}</td>
                <td>${venta.fecha}</td>
                <td>${venta.cantidad}</td>
                <td>${venta.total}</td>
                <td><button onclick="eliminarVenta('${venta.folio}')">Eliminar</button></td>
            </tr>`;
            tabla.innerHTML += row;
        });
    } catch (error) {
        console.error('Error obteniendo ventas:', error);
    }
}

async function agregarVenta() {
    const folio = document.getElementById('folio').value;
    const fecha = document.getElementById('fecha').value;
    const cantidad = parseFloat(document.getElementById('cantidad').value);
    const total = parseFloat(document.getElementById('total').value);

    if (!folio || !fecha || isNaN(cantidad) || isNaN(total)) {
        alert('Todos los campos son obligatorios');
        return;
    }

    const nuevaVenta = { folio, fecha, cantidad, total };

    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(nuevaVenta)
        });

        if (response.ok) {
            obtenerVentas();
        } else {
            alert('Error al agregar la venta');
        }
    } catch (error) {
        console.error('Error agregando venta:', error);
    }
}

async function eliminarVenta(folio) {
    try {
        const response = await fetch(`${apiUrl}/${folio}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            obtenerVentas();
        } else {
            alert('Error al eliminar la venta');
        }
    } catch (error) {
        console.error('Error eliminando venta:', error);
    }
}

document.addEventListener('DOMContentLoaded', obtenerVentas);
