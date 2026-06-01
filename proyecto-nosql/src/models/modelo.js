// Archivo simulado para Sprint sin Node.js

// Array para almacenar las quejas (simulando base de datos)
let quejas = [
    {
        id: 1,
        titulo: "Título de la Queja",
        descripcion: "Descripción de la queja...",
        categoria: "servicio",
        estado: "abierto",
        fecha: "24/11/2025"
    },
    {
        id: 2,
        titulo: "Segunda Queja",
        descripcion: "Descripción de la queja...",
        categoria: "producto",
        estado: "en-proceso",
        fecha: "23/11/2025"
    },
    {
        id: 3,
        titulo: "Tercera Queja",
        descripcion: "Descripción de la queja...",
        categoria: "otro",
        estado: "cerrado",
        fecha: "20/11/2025"
    }
];

// Variables globales
let quejasFiltradas = [...quejas];

// CREAR - Agregar nueva queja
document.getElementById('formQueja').addEventListener('submit', function(e) {
    e.preventDefault();

    const nuevoId = Math.max(...quejas.map(q => q.id), 0) + 1;
    const fechaActual = new Date().toLocaleDateString('es-ES');

    const nuevaQueja = {
        id: nuevoId,
        titulo: document.getElementById('titulo').value,
        descripcion: document.getElementById('descripcion').value,
        categoria: document.getElementById('categoria').value,
        estado: 'abierto',
        fecha: fechaActual
    };

    quejas.push(nuevaQueja);
    quejasFiltradas = [...quejas];
    
    this.reset();
    mostrarQuejas(quejas);
    alert('Queja registrada exitosamente');
});

// LEER - Mostrar todas las quejas
function mostrarQuejas(listQuejas = quejas) {
    const container = document.getElementById('quejasList');
    container.innerHTML = '';

    if (listQuejas.length === 0) {
        container.innerHTML = '<p class="no-quejas">No hay quejas registradas</p>';
        return;
    }

    listQuejas.forEach(queja => {
        const quejaCard = document.createElement('div');
        quejaCard.className = `queja-card ${queja.estado}`;
        quejaCard.innerHTML = `
            <div class="queja-header">
                <h3>${queja.titulo}</h3>
                <span class="badge badge-${queja.estado}">${capitalizarEstado(queja.estado)}</span>
            </div>
            <div class="queja-body">
                <p><strong>ID:</strong> #${String(queja.id).padStart(3, '0')}</p>
                <p><strong>Categoría:</strong> ${capitalizarPalabra(queja.categoria)}</p>
                <p><strong>Descripción:</strong> ${queja.descripcion}</p>
                <p><strong>Fecha:</strong> ${queja.fecha}</p>
            </div>
            <div class="queja-footer">
                <button class="btn btn-sm btn-info" onclick="verDetalles(${queja.id})">Ver Detalles</button>
                <button class="btn btn-sm btn-warning" onclick="editarQueja(${queja.id})">Editar</button>
                <button class="btn btn-sm btn-danger" onclick="eliminarQueja(${queja.id})">Eliminar</button>
            </div>
        `;
        container.appendChild(quejaCard);
    });
}

// VER DETALLES - Modal de detalles
function verDetalles(id) {
    const queja = quejas.find(q => q.id === id);
    if (queja) {
        alert(`
ID: #${String(queja.id).padStart(3, '0')}
Título: ${queja.titulo}
Descripción: ${queja.descripcion}
Categoría: ${capitalizarPalabra(queja.categoria)}
Estado: ${capitalizarEstado(queja.estado)}
Fecha: ${queja.fecha}
        `);
    }
}

// ACTUALIZAR - Editar queja
function editarQueja(id) {
    const queja = quejas.find(q => q.id === id);
    if (!queja) return;

    const nuevoTitulo = prompt('Nuevo título:', queja.titulo);
    if (nuevoTitulo === null) return;

    const nuevaDescripcion = prompt('Nueva descripción:', queja.descripcion);
    if (nuevaDescripcion === null) return;

    const nuevoEstado = prompt('Nuevo estado (abierto/en-proceso/cerrado):', queja.estado);
    if (nuevoEstado === null) return;

    if (['abierto', 'en-proceso', 'cerrado'].includes(nuevoEstado)) {
        queja.titulo = nuevoTitulo;
        queja.descripcion = nuevaDescripcion;
        queja.estado = nuevoEstado;
        
        mostrarQuejas(quejas);
        alert('Queja actualizada exitosamente');
    } else {
        alert('Estado inválido');
    }
}

// ELIMINAR - Borrar queja
function eliminarQueja(id) {
    if (confirm('¿Estás seguro de que deseas eliminar esta queja?')) {
        quejas = quejas.filter(q => q.id !== id);
        quejasFiltradas = [...quejas];
        mostrarQuejas(quejas);
        alert('Queja eliminada exitosamente');
    }
}

// FILTRAR - Aplicar filtros
function aplicarFiltros() {
    const buscar = document.getElementById('buscar').value.toLowerCase();
    const estado = document.getElementById('filtroEstado').value;

    quejasFiltradas = quejas.filter(queja => {
        const coincideTitulo = queja.titulo.toLowerCase().includes(buscar);
        const coincideEstado = estado === '' || queja.estado === estado;
        return coincideTitulo && coincideEstado;
    });

    mostrarQuejas(quejasFiltradas);
}

// FUNCIONES AUXILIARES
function capitalizarPalabra(palabra) {
    return palabra.charAt(0).toUpperCase() + palabra.slice(1);
}

function capitalizarEstado(estado) {
    const estadoMap = {
        'abierto': 'Abierto',
        'en-proceso': 'En Proceso',
        'cerrado': 'Cerrado'
    };
    return estadoMap[estado] || estado;
}

// Permitir filtrado en tiempo real
document.getElementById('buscar').addEventListener('keyup', aplicarFiltros);
document.getElementById('filtroEstado').addEventListener('change', aplicarFiltros);

// Inicializar al cargar
window.addEventListener('DOMContentLoaded', function() {
    mostrarQuejas(quejas);
});
