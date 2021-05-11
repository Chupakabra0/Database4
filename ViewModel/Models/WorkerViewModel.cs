﻿using ConsoleDBTest.Models;
using ConsoleDBTest.Utils.StringUtils;

namespace ConsoleDBTest.ViewModels {
    public class WorkerViewModel {
        public WorkerViewModel(Worker worker) {
            this.Id         = worker.Id;
            this.Name       = worker.Name;
            this.Surname    = worker.Surname;
            this.Patronymic = worker.Patronymic;
            this.IsActive   = worker.IsActive;
        }

        public int    Id         { get; set; }
        public string Name       { get; set; }
        public string Surname    { get; set; }
        public string Patronymic { get; set; }
        public bool   IsActive   { get; set; }

        public override string ToString() => StringUtils.GetPersonName(this.Name, this.Surname, this.Patronymic);
    }
}