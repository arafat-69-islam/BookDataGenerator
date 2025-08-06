export async function exportBooksToCsv(params) {
    const url = new URL('/api/export/csv', window.location.origin);
    Object.entries(params).forEach(([key, value]) => {
        url.searchParams.append(key, value);
    });
    window.location.href = url;
}