﻿namespace TenApplication.Dtos.CatDTOModels
{
    public class CatDto
    {
        public int CatId { get; set; }

        //RELATIONS  
        public string CCtr { get; set; }
        public string ActTyp { get; set; }

        public List<CatRecordDto> CatRecords { get; set; }
    }
}