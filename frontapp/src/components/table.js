import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';

function CocktailTable({ data }) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Name</TableCell>
          <TableCell>Ingredient</TableCell>
          <TableCell>Dose</TableCell>
          <TableCell>Unit</TableCell>
        </TableRow>
      </TableHead>
      <tbody>
        {data.map((cocktail) => (
          <TableRow key={cocktail.id}>
            <TableCell>{cocktail.name}</TableCell>
            {cocktail.coctailIngridients.map((ingredient) => (
              <React.Fragment key={ingredient.id}>
                <TableCell>{ingredient.name}</TableCell>
                <TableCell>{ingredient.dose}</TableCell>
                <TableCell>{ingredient.unit}</TableCell>
              </React.Fragment>
            ))}
          </TableRow>
        ))}
      </tbody>
    </Table>
  );
}

export default CocktailTable;
