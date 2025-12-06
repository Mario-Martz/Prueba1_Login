using Prueba1_Login.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Prueba1_Login.Domain.Interfaces
{
    public interface ICalendarioSorteosRepository
    {
        void InsertarLista(IEnumerable<CalendarioSorteos> lista);
        IEnumerable<CalendarioSorteos> Buscar(DateTime inicio, DateTime fin, int tipoSorteo);
    }

}

