function abrirLogin() {
  document.getElementById('modalLogin').style.display = 'flex';
}

function fecharLogin() {
  document.getElementById('modalLogin').style.display = 'none';
}

// Simulação de login (até você ter a API real)
document.getElementById('formLogin').addEventListener('submit', function (e) {
  e.preventDefault();

  const email = document.getElementById('loginEmail').value;
  const senha = document.getElementById('loginSenha').value;

  // Simulando usuário do backend
  // Depois, aqui você vai usar um fetch na API real
  let tipoUsuario = 1; // 1 = aluno | 2 = funcionário

  if (email === "func@uni.com" && senha === "123") {
    tipoUsuario = 2;
  }

  // Salva no localStorage
  localStorage.setItem('tipoUsuario', tipoUsuario);

  alert("Login realizado com sucesso!");
  fecharLogin();
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

// Executa ao carregar a página
window.addEventListener('DOMContentLoaded', atualizarMenu);
