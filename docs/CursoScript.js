 function abrirModal(curso) {
    document.getElementById('curso').value = curso;
    document.getElementById('modalMatricula').style.display = 'flex';
  }

  function fecharModal() {
    document.getElementById('modalMatricula').style.display = 'none';
  }

  const form = document.getElementById('formMatricula');

  form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const data = {
      nome: document.getElementById('nome').value,
      email: document.getElementById('email').value,
      telefone: document.getElementById('telefone').value,
      curso: document.getElementById('curso').value
    };

    try {
      const response = await fetch('https://carlosdv11.bsite.net/Api/V1/Matricula/Create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
      });

      if (response.ok) {
        alert('Matrícula enviada com sucesso!');
        form.reset();
        fecharModal();
      } else {
        alert('Erro ao enviar matrícula.');
      }
    } catch (error) {
      console.error('Erro na requisição:', error);
      alert('Erro na requisição.');
    }
  });