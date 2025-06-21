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
    const response = await fetch('https://carlosdv11.bsite.net/Api/V1/User/Login', {
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
  const logoutBtn = document.getElementById('logoutBtn');
  const loginLi = document.getElementById('loginLi');
  const menuCursos = document.getElementById('menuCursos');
  const menuMeusCursos = document.getElementById('menuMeusCursos');

  if (tipo === "2") { // Funcionario
    adminMenu.style.display = 'inline-block';
    menuCursos.style.display = 'inline-block';
    menuMeusCursos.style.display = 'none';
  } else if (tipo === "1") { // Aluno
    adminMenu.style.display = 'none';
    menuCursos.style.display = 'none';
    menuMeusCursos.style.display = 'inline-block';
  } else {
    adminMenu.style.display = 'none';
    menuCursos.style.display = 'inline-block';
    menuMeusCursos.style.display = 'none';
  }

  if (tipo) {
    logoutBtn.style.display = 'inline-block';
    loginLi.style.display = 'none';
  } else {
    logoutBtn.style.display = 'none';
    loginLi.style.display = 'inline-block';
  }
}


function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('tipoUsuario');
  atualizarMenu();
  alert('Logout realizado com sucesso!');
  fecharLogin();
}





