const tipo = localStorage.getItem('tipoUsuario');
if (tipo !== "2") {
  alert("Acesso restrito.");
  window.location.href = "index.html";
}

const token = localStorage.getItem('token');
const mensagemEl = document.getElementById('mensagem');
const tableBody = document.querySelector('#matriculasTable tbody');

async function carregarMatriculas() {
  try {
    const res = await fetch('https://localhost:7124/Api/V1/Matricula/Get-Matriculas-Pendentes', {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    if (!res.ok) {
      throw new Error('Erro ao carregar matrículas.');
    }
    const matriculas = await res.json();

    // Limpa tabela antes de popular
    tableBody.innerHTML = '';

    if (matriculas.length === 0) {
      tableBody.innerHTML = '<tr><td colspan="5">Nenhuma matrícula pendente.</td></tr>';
      return;
    }

    matriculas.forEach(m => {
      const tr = document.createElement('tr');

      tr.innerHTML = `
        <td>${m.nome}</td>
        <td>${m.email}</td>
        <td>${m.telefone}</td>
        <td>${m.curso}</td>
        <td>
          <button class="aprovar" onclick="aprovarMatricula(${m.id})">Aprovar</button>
          <button class="reprovar" onclick="reprovarMatricula(${m.id})">Reprovar</button>
        </td>
      `;

      tableBody.appendChild(tr);
    });
  } catch (err) {
    mensagemEl.style.color = 'red';
    mensagemEl.textContent = err.message;
  }
}

async function aprovarMatricula(id) {
  try {
    const res = await fetch(`https://localhost:7124/Api/V1/Matricula/Aprovar/${id}`, {
      method: 'PUT',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });

    if (!res.ok) {
      throw new Error('Erro ao aprovar matrícula.');
    }

    mensagemEl.style.color = 'green';
    mensagemEl.textContent = 'Matrícula aprovada com sucesso!';

    carregarMatriculas(); // recarrega lista
  } catch (err) {
    mensagemEl.style.color = 'red';
    mensagemEl.textContent = err.message;
  }
}

async function reprovarMatricula(id) {
  try {
    const res = await fetch(`https://localhost:7124/Api/V1/Matricula/Rejeitar/${id}`, {
      method: 'PUT',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });

    if (!res.ok) {
      throw new Error('Erro ao reprovar matrícula.');
    }

    mensagemEl.style.color = 'green';
    mensagemEl.textContent = 'Matrícula reprovada com sucesso!';

    carregarMatriculas(); // recarrega lista
  } catch (err) {
    mensagemEl.style.color = 'red';
    mensagemEl.textContent = err.message;
  }
}

// Carrega matrículas assim que a página estiver pronta
window.addEventListener('DOMContentLoaded', () => {
  if (!token) {
    alert('Você precisa estar logado para acessar essa página.');
    window.location.href = 'index.html';
    return;
  }
  carregarMatriculas();
});
