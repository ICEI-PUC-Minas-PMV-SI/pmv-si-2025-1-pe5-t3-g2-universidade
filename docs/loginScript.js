function abrirLogin() {
  document.getElementById('modalLogin').style.display = 'flex';
}

function fecharLogin() {
  document.getElementById('modalLogin').style.display = 'none';
}

async function handleLogin(event) {
  event.preventDefault();

  const email = document.getElementById('loginEmail').value;
  const senha = document.getElementById('loginSenha').value;

  try {
    const response = await fetch('https://localhost:7124/Api/V1/User/Login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, senha })
    });

    if (!response.ok) {
      alert("Email ou senha inválidos.");
      return;
    }

    const result = await response.json();

    localStorage.setItem('token', result.token);

    const payloadBase64 = result.token.split('.')[1];
    const payload = JSON.parse(atob(payloadBase64));
    const tipoUsuario = payload.role === "Funcionario" ? 2 : 1;

    localStorage.setItem('tipoUsuario', tipoUsuario.toString());

    alert("Login realizado com sucesso!");
    fecharLogin();
    atualizarMenu();

  } catch (err) {
    console.error(err);
    alert("Erro ao tentar logar. Verifique sua conexão.");
  }
}

window.addEventListener('DOMContentLoaded', () => {
  document.getElementById('formLogin').addEventListener('submit', handleLogin);
  atualizarMenu();
});

function atualizarMenu() {
  const tipo = localStorage.getItem('tipoUsuario');
  const adminMenu = document.getElementById('adminMenu');

  if (tipo === "2") {
    adminMenu.style.display = 'inline-block';
  } else {
    adminMenu.style.display = 'none';
  }
}

function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('tipoUsuario');
  atualizarMenu();
  alert('Logout realizado com sucesso!');
  fecharLogin();
}

// Atualiza o menu para mostrar ou esconder logout/login/admin
function atualizarMenu() {
  const tipo = localStorage.getItem('tipoUsuario');
  const adminMenu = document.getElementById('adminMenu');
  const logoutBtn = document.getElementById('logoutBtn');
  const loginLi = document.getElementById('loginLi');

  if (tipo === "2") {
    adminMenu.style.display = 'inline-block';
  } else {
    adminMenu.style.display = 'none';
  }

  if (tipo) {
    logoutBtn.style.display = 'inline-block';
    loginLi.style.display = 'none';
  } else {
    logoutBtn.style.display = 'none';
    loginLi.style.display = 'inline-block';
  }
}

